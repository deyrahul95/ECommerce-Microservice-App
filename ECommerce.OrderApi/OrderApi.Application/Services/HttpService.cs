using System.Net;
using System.Net.Http.Json;
using ECommerce.Shared.Models;
using ECommerce.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using OrderApi.Application.DTOs;
using OrderApi.Application.Services.Interfaces;

namespace OrderApi.Application.Services;

public class HttpService(
    IHttpClientFactory httpClientFactory,
    ILoggerService logger) : IHttpService
{
    public const string HTTP_CLIENT_NAME = "OrderServiceClient";

    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HTTP_CLIENT_NAME);

    public async Task<ProductDTO?> GetProduct(Guid productId, CancellationToken token = default)
    {
        logger.LogInformation($"Fetching product data. Id: {productId}");

        var response = await _httpClient.GetAsync(
            requestUri: $"api/products/{productId}",
            cancellationToken: token);

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
            $"Product fetched completed. Id: {productId}, StatusCode: {result.StatusCode}, Message: {result.Message}"
        );
        return result.Data;
    }

    public async Task<UserDTO?> GetUser(Guid userId, CancellationToken token = default)
    {
        logger.LogInformation($"Fetching user data. Id: {userId}");

        var response = await _httpClient.GetAsync(
            requestUri: $"api/users/{userId}",
            cancellationToken: token);

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
            $"User fetched completed. Id: {userId}, StatusCode: {result.StatusCode}, Message: {result.Message}"
        );
        return result.Data;
    }
}
