using System.Net;
using AuthApi.Users.DTOs;
using ECommerce.Shared.Models;

namespace AuthApi.Users.Services;

public static class AuthResults
{
    public static ServiceResult<JWTTokenDTO> INTERNAL_SERVICE_FAILURE => new(
        HttpStatusCode.InternalServerError,
        "Internal service failure. Please try again after some time.");

    public static ServiceResult USER_EMAIL_CONFLICT(string email) => new(
        HttpStatusCode.Conflict,
        $"User with email: {email} already exists");

    public static ServiceResult USER_REGISTERED => new(
        HttpStatusCode.OK,
        "User registered successfully");

    public static ServiceResult<JWTTokenDTO> USER_LOGGED_IN(JWTTokenDTO data) => new(
        HttpStatusCode.OK,
        "User logged in successfully",
        data);

    public static ServiceResult<JWTTokenDTO> INVALID_CREDENTIAL => new(
        HttpStatusCode.BadRequest,
        "Invalid credentials. Please try again.");
}
