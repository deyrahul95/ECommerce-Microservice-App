using System.Net;
using System.Security.Claims;
using AuthApi.Users.Models;
using AuthApi.Users.Services.Interfaces;
using ECommerce.Shared.Models;
using ECommerce.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, ILoggerService logger) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken token = default)
    {
        if (ModelState.IsValid is false)
        {
            return BadRequest(ModelState);
        }

        var response = await authService.Register(request, token);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(response);
        }

        if (response.StatusCode == HttpStatusCode.Conflict)
        {
            return Conflict(response);
        }

        return StatusCode((int)response.StatusCode, response.Message);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken token = default)
    {
        if (ModelState.IsValid is false)
        {
            return BadRequest(ModelState);
        }

        var response = await authService.Login(request, token);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(response);
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return BadRequest(response);
        }

        return StatusCode((int)response.StatusCode, response.Message);
    }

    [HttpGet]
    [Route("verify")]
    [Authorize]
    public IActionResult Verify()
    {
        var jwtClaimsData = GetJwtClaimsData();

        if (string.IsNullOrEmpty(jwtClaimsData.UserId)
            || string.IsNullOrEmpty(jwtClaimsData.Name)
            || string.IsNullOrEmpty(jwtClaimsData.Email))
        {
            logger.LogWarning($"Unauthorized Request. Id: {jwtClaimsData.UserId ?? "N/A"}, Email: {jwtClaimsData.Email ?? "N/A"}");
            return Unauthorized();
        }

        return Ok(new ServiceResult<JwtClaimsData>(
            HttpStatusCode.OK,
            "User verified",
            jwtClaimsData));
    }

    private JwtClaimsData GetJwtClaimsData()
    {
        var userId = User.FindFirst("Id")?.Value;
        var name = User.FindFirst(ClaimTypes.Name)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        return new JwtClaimsData(
            UserId: userId,
            Name: name,
            Email: email,
            Roles: roles
        );
    }
}
