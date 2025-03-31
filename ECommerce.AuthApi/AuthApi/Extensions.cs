namespace AuthApi;

public static class Extensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {

        return services;
    }

    public static IApplicationBuilder UseApiPolicy(this IApplicationBuilder app)
    {

        return app;
    }
}
