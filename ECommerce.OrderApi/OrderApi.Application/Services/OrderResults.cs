using System.Collections;
using System.Net;
using ECommerce.Shared.Models;

namespace OrderApi.Application.Services;

public static class OrderResults<T> where T : class
{
    public static ServiceResult<T> INTERNAL_SERVICE_FAILURE => new(
        HttpStatusCode.InternalServerError,
        "Internal service failure. Please try again after some time.");

    public static ServiceResult<T> ORDER_NOT_FOUND(Guid id) => new(
        HttpStatusCode.NotFound,
        $"No order found with Id: {id}");

    public static ServiceResult<T> PRODUCT_NOT_FOUND(Guid id) => new(
        HttpStatusCode.NotFound,
        $"No product found with Id: {id}");

    public static ServiceResult<T> USER_NOT_FOUND(Guid id) => new(
        HttpStatusCode.NotFound,
        $"No user found with Id: {id}");

    public static ServiceResult<T> ORDER_FETCHED(T data) => new(
        HttpStatusCode.OK,
        data is IList ? "Orders fetched successfully" : "Order fetched successfully",
        data);
}
