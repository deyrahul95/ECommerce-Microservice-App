using AuthApi.Users.DTOs;
using AuthApi.Users.Models;
using ECommerce.Shared.Models;

namespace AuthApi.Users.Services.Interfaces;

public interface IAuthService
{
    public Task<ServiceResult> Register(RegisterRequest request, CancellationToken token = default);
    public Task<ServiceResult<JWTTokenDTO>> Login(LoginRequest request, CancellationToken token = default);
}
