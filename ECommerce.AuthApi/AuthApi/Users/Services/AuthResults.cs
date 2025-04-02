using System.Net;
using ECommerce.Shared.Models;

namespace AuthApi.Users.Services;

public static class AuthResults
{
    public static ServiceResult<string> INTERNAL_SERVICE_FAILURE => new(
        HttpStatusCode.InternalServerError,
        "Internal service failure. Please try again after some time.");

    public static ServiceResult USER_EMAIL_CONFLICT(string email) => new(
        HttpStatusCode.Conflict,
        $"User with email: {email} already exists");

    public static ServiceResult USER_REGISTERED => new(
        HttpStatusCode.OK,
        "User registered successfully");

    public static ServiceResult<string> USER_LOGGED_IN(string data) => new(
        HttpStatusCode.OK,
        "User logged in successfully",
        data);

    public static ServiceResult<string> INVALID_CREDENTIAL => new(
        HttpStatusCode.BadRequest,
        "Invalid credentials. Please try again.");
}
