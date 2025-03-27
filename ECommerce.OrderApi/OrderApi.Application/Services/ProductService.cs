using System.Net;
using System.Net.Http.Json;
using ECommerce.Shared.Models;
using ECommerce.Shared.Services.Interfaces;
using OrderApi.Application.DTOs;
using OrderApi.Application.Services.Interfaces;
using Polly;

namespace OrderApi.Application.Services;

public class ProductService(
    HttpClient httpClient,
    ResiliencePipeline<string> resiliencePipeline,
    ILoggerService logger) : IProductService
{
    public async Task<ProductDTO?> GetProduct(Guid id)
    {
        var uri = new Uri( $"/api/products/{id}");

        logger.LogInformation($"Fetching product data. Id: {id}, Uri: {uri}");
        var response = await httpClient.GetAsync(uri);

        if (response.IsSuccessStatusCode == false)
        {
            logger.LogWarning($"Product fetched failed. StatusCode: {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<ProductDTO>>();

        if (result == null || result.StatusCode != HttpStatusCode.OK)
        {
            logger.LogWarning($"Result has failed status code. StatusCode: {result?.StatusCode.ToString() ?? "N?A"}, Message: {result?.Message ?? "N/A"}");
            return null;
        }

        logger.LogInformation($"Product fetched completed. Id: {id}, Uri: {uri}, StatusCode: {result.StatusCode}, Message: {result.Message}");

        return result.Data;
    }
}
