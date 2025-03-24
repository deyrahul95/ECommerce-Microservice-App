using System.Text.Json;
using ECommerce.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Shared.Middlewares;

public class GlobalExceptionHandler(RequestDelegate next, ILoggerService logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
            await CheckResponses(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task CheckResponses(HttpContext context)
    {
        switch (context.Response.StatusCode)
        {
            case StatusCodes.Status429TooManyRequests:
                await ModifyResponseHeader(context, "Too many requests made. Please try after some times.");
                break;

            case StatusCodes.Status401Unauthorized:
                await ModifyResponseHeader(context, "You are not authorized to access this content.");
                break;

            case StatusCodes.Status403Forbidden:
                await ModifyResponseHeader(context, "You don't have required access.");
                break;

            default:
                return;
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        logger.LogError("An unhandled exception has occurred.", ex);

        if (ex is TaskCanceledException || ex is TimeoutException)
        {
            context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
            await ModifyResponseHeader(context, "Request timeout... Try again!");
            return;
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await ModifyResponseHeader(context, "An unexpected error occurred. Please try again later.");
        return;
    }

    private static async Task ModifyResponseHeader(HttpContext context, string message)
    {
        context.Response.ContentType = "application/json";

        var result = new
        {
            StatusCode = context.Response.StatusCode,
            Message = message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(result), CancellationToken.None);
        return;
    }
}
