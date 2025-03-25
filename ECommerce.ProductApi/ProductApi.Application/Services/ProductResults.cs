using System.Collections;
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

    public static ServiceResult<T> PRODUCT_FETCHED(T data) => new(
        HttpStatusCode.OK,
        data is IList ? "Products fetched successfully" : "Product fetched successfully",
        data);

    public static ServiceResult<T> PRODUCT_NOT_FOUND(Guid id) => new(
        HttpStatusCode.NotFound,
        $"No product found with Id: {id}");
}