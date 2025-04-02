using System.Net;
using AuthApi.Users.Models;
using AuthApi.Users.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
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
}
