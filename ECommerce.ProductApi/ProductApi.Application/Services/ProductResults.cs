using System.Net;
using ECommerce.Shared.Models;

namespace ProductApi.Application.Services;

public static class ProductResults<T> where T : class
{
    public static ServiceResult<T> INTERNAL_SERVICE_FAILURE => new(
        HttpStatusCode.InternalServerError,
        "Internal service failure. Please try again after some time.");

    public static ServiceResult<T> PRODUCT_NAME_CONFLICT(string name) => new(
        HttpStatusCode.Conflict,
        $"Product with name: {name} already exists");

    public static ServiceResult<T> PRODUCT_CREATED(T data) => new(
        HttpStatusCode.Created,
        "Product created successfully",
        data);
}