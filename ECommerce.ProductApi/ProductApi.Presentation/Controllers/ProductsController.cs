using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.Models;
using ProductApi.Application.Services.Interfaces;

namespace ProductApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token = default)
    {
        var response = await productService.GetAllProducts(token);

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
        var response = await productService.GetProduct(id, token);

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
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken token = default)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        var response = await productService.CreateProduct(request, token);

        if (response.StatusCode == HttpStatusCode.Created)
        {
            var url = Url.Action("Get", "Products", new { id = response.Data?.Id }, Request.Scheme);

            return Created(url, response);
        }

        if (response.StatusCode == HttpStatusCode.Conflict)
        {
            return Conflict(response);
        }

        return StatusCode((int)response.StatusCode, response.Message);
    }
}
