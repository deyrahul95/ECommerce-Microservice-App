using System.Net;
using System.Net.Http.Json;
using ECommerce.Shared.Models;
using ECommerce.Shared.Services.Interfaces;
using OrderApi.Application.DTOs;
using OrderApi.Application.Services.Interfaces;

namespace OrderApi.Application.Services;

public class HttpService(
    HttpClient httpClient,
    ILoggerService logger) : IHttpService
{
    public async Task<ProductDTO?> GetProduct(Guid productId, CancellationToken token = default)
    {
        var uri = new Uri($"/api/products/{productId}");

        logger.LogInformation($"Fetching product data. Id: {productId}, Uri: {uri}");
        var response = await httpClient.GetAsync(uri, token);

        if (response.IsSuccessStatusCode == false)
        {
            logger.LogWarning($"Product fetched failed. StatusCode: {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<ProductDTO>>(token);

        if (result == null || result.StatusCode != HttpStatusCode.OK)
        {
            logger.LogWarning(
                $"Result has failed status code. StatusCode: {result?.StatusCode.ToString() ?? "N?A"}, Message: {result?.Message ?? "N/A"}"
            );
            return null;
        }

        logger.LogInformation(
            $"Product fetched completed. Id: {productId}, Uri: {uri}, StatusCode: {result.StatusCode}, Message: {result.Message}"
        );
        return result.Data;
    }

    public async Task<UserDTO?> GetUser(Guid userId, CancellationToken token = default)
    {
        var uri = new Uri($"/api/users/{userId}");

        logger.LogInformation($"Fetching user data. Id: {userId}, Uri: {uri}");
        var response = await httpClient.GetAsync(uri, token);

        if (response.IsSuccessStatusCode == false)
        {
            logger.LogWarning($"User fetched failed. StatusCode: {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<UserDTO>>(token);

        if (result == null || result.StatusCode != HttpStatusCode.OK)
        {
            logger.LogWarning(
                $"Result has failed status code. StatusCode: {result?.StatusCode.ToString() ?? "N?A"}, Message: {result?.Message ?? "N/A"}"
            );
            return null;
        }

        logger.LogInformation(
            $"User fetched completed. Id: {userId}, Uri: {uri}, StatusCode: {result.StatusCode}, Message: {result.Message}"
        );
        return result.Data;
    }
}
