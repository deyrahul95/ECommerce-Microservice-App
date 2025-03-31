using System.Net;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Application.Models;
using OrderApi.Application.Services.Interfaces;

namespace OrderApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token = default)
    {
        var response = await orderService.GetAllOrders(token);

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
        var response = await orderService.GetOrder(id, token);

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken token = default)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        var response = await orderService.CreateOrder(request, token);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound(response);
        }

        if (response.StatusCode == HttpStatusCode.Created)
        {
            var url = Url.Action("Get", "Orders", new { id = response.Data?.Id }, Request.Scheme);

            return Created(url, response);
        }

        return StatusCode((int)response.StatusCode, response.Message);
    }

    [HttpGet]
    [Route("client/{id:Guid}")]
    public async Task<IActionResult> GetClientOrders([FromRoute] Guid id, CancellationToken token = default)
    {
        var response = await orderService.GetClientOrders(id, token);

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
    [Route("{id:Guid}/details")]
    public async Task<IActionResult> GetDetails([FromRoute] Guid id, CancellationToken token = default)
    {
        var response = await orderService.GetOrderDetails(id, token);

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
}
