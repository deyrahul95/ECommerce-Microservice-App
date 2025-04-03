using System.Net;
using System.Security.Claims;
using AuthApi.Users.Enums;
using AuthApi.Users.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll(CancellationToken token = default)
    {
        var response = await userService.GetAllUsers(token);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(response);
        }

        return StatusCode((int)response.StatusCode, response.Message);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
    {
        if (HasAdminRole() == false && IsSameUserId(id) == false)
        {
            return Unauthorized();
        }

        var response = await userService.GetUser(id, token);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(response);
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound(response);
        }

        return StatusCode((int)response.StatusCode, response.Message);
    }

    [HttpGet]
    [Route("{id:Guid}/make-admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MakeAdmin([FromRoute] Guid id, CancellationToken token = default)
    {
        var response = await userService.MakeAdmin(id, token);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Ok(response);
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound(response);
        }

        if (response.StatusCode == HttpStatusCode.Conflict)
        {
            return Conflict(response);
        }

        return StatusCode((int)response.StatusCode, response.Message);
    }

    private bool HasAdminRole()
    {
        var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        return roles.Contains(AppUserRole.Admin.ToString());
    }

    private bool IsSameUserId(Guid id)
    {
        var userId = User.FindFirst("Id")?.Value;

        return string.Equals(userId, id.ToString(), StringComparison.OrdinalIgnoreCase);
    }
}
