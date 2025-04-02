using System.Net;
using AuthApi.Users.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    [Authorize]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
    {
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
}
