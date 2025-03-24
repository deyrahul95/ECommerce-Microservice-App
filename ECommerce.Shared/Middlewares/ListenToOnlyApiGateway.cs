using Microsoft.AspNetCore.Http;

namespace ECommerce.Shared.Middlewares;

public class ListenToOnlyApiGateway(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var signedHeader = context.Request.Headers["Api-Gateway"];

        if (signedHeader.FirstOrDefault() is null)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            return;
        }

        await next(context);
    }
}
