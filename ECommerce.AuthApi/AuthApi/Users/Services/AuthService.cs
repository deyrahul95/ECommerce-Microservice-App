using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthApi.Users.Configs;
using AuthApi.Users.DTOs;
using AuthApi.Users.Entities;
using AuthApi.Users.Enums;
using AuthApi.Users.Models;
using AuthApi.Users.Repositories.Interfaces;
using AuthApi.Users.Services.Interfaces;
using ECommerce.Shared.Models;
using ECommerce.Shared.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Users.Services;

public class AuthService(
    IUserRepository userRepository,
    IOptions<Authentication> authOptions,
    ILoggerService logger) : IAuthService
{
    private readonly Authentication authConfig = authOptions.Value;

    public async Task<ServiceResult<JWTTokenDTO>> Login(LoginRequest request, CancellationToken token = default)
    {
        try
        {
            var user = await userRepository.GetUserByEmail(request.Email, token);

            if (user is null)
            {
                logger.LogWarning($"Requested User not found. Email ID: {request.Email}");
                return AuthResults.INVALID_CREDENTIAL;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (isValidPassword is false)
            {
                logger.LogWarning($"Requested User password is not valid. Email ID: {request.Email}");
                return AuthResults.INVALID_CREDENTIAL;
            }

            var jwtToken = GenerateJWTToken(user);

            return AuthResults.USER_LOGGED_IN(jwtToken);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to logged in user. Email: {request.Email}", ex);
            return AuthResults.INTERNAL_SERVICE_FAILURE;
        }
    }

    public async Task<ServiceResult> Register(RegisterRequest request, CancellationToken token = default)
    {
        try
        {
            var existingUser = await userRepository.GetUserByEmail(request.Email, token);

            if (existingUser is not null)
            {
                logger.LogWarning($"Requested User already exists. Email ID: {request.Email}");
                return AuthResults.USER_EMAIL_CONFLICT(request.Email);
            }

            var user = request.ToEntity();
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = await userRepository.CreateAsync(user, token);

            if (newUser is null || newUser.Id == Guid.Empty)
            {
                return AuthResults.INTERNAL_SERVICE_FAILURE;
            }

            return AuthResults.USER_REGISTERED;
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to register user. Name: {request.Name}, Email: {request.Email}", ex);
            return AuthResults.INTERNAL_SERVICE_FAILURE;
        }
    }

    private JWTTokenDTO GenerateJWTToken(AppUser user)
    {
        var key = Encoding.UTF8.GetBytes(authConfig.Key);
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim> {
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.Role, user.Role ?? AppUserRole.Guest.ToString()),
        };

        var expiresIn = DateTime.UtcNow.AddHours(2);

        var token = new JwtSecurityToken(
            issuer: authConfig.Issuer,
            audience: authConfig.Audience,
            claims: claims,
            expires: expiresIn,
            signingCredentials: credentials);
        
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new JWTTokenDTO(jwtToken, expiresIn);
    }
}