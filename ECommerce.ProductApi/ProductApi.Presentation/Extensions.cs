using ECommerce.Shared;
using ProductApi.Application;
using ProductApi.Infrastructure;
using ProductApi.Infrastructure.DB;

namespace ProductApi.Presentation;

public static class Extensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddInfrastructureServices();

        services.AddSharedServices<ProductDbContext>(configuration);

        return services;
    }

    public static IApplicationBuilder UseApiPolicies(this IApplicationBuilder app)
    {
        app.UseSharedPolicies();

        return app;
    }
}
