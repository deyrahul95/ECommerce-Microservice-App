using System.Collections;
using System.Net;
using ECommerce.Shared.Models;

namespace AuthApi.Users.Services;

public static class UserResults<T> where T : class
{
    public static ServiceResult<T> INTERNAL_SERVICE_FAILURE => new(
        HttpStatusCode.InternalServerError,
        "Internal service failure. Please try again after some time.");

    public static ServiceResult<T> USER_NOT_FOUND(Guid userId) => new(
        HttpStatusCode.NotFound,
        $"No user found with Id: {userId}.");

    public static ServiceResult<T> USER_FETCHED(T data) => new(
        HttpStatusCode.OK,
        data is IList ? "Users fetched successfully" : "User fetched successfully",
        data);

    public static ServiceResult<T> USER_ALREADY_ADMIN(Guid userId) => new(
        HttpStatusCode.Conflict,
        $"User ID {userId} already has the Admin role.");

    public static ServiceResult<T> USER_UPDATED(T data) => new(
        HttpStatusCode.OK,
        "User updated successfully",
        data);
}
