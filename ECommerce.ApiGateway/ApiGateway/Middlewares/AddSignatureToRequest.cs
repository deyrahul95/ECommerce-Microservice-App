namespace ApiGateway.Middlewares;

public class AddSignatureToRequest(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers["Api-Gateway"] = "Signed";
        await next(context);
    }
}
