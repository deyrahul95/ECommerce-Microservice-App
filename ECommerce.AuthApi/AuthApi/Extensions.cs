using AuthApi.Users.Configs;

namespace AuthApi;

public static class Extensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Authentication>(configuration.GetSection("Authentication"));

        return services;
    }

    public static IApplicationBuilder UseApiPolicy(this IApplicationBuilder app)
    {

        return app;
    }
}
