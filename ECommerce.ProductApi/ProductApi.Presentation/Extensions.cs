using ECommerce.Shared;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application;
using ProductApi.Infrastructure;
using ProductApi.Infrastructure.DB;

namespace ProductApi.Presentation;

public static class Extensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add database context
        var sqliteConnection = configuration.GetConnectionString("SqliteConnection");
        services.AddDbContext<ProductDbContext>(options => options.UseSqlite(sqliteConnection));

        services.AddSharedServices(configuration);

        services.AddInfrastructureServices();
        services.AddApplicationServices();

        return services;
    }

    public static IApplicationBuilder UseApiPolicies(this IApplicationBuilder app)
    {
        app.UseSharedPolicies();

        return app;
    }
}
