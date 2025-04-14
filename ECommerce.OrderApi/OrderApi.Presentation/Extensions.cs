using ECommerce.Shared;
using Microsoft.EntityFrameworkCore;
using OrderApi.Application;
using OrderApi.Infrastructure;
using OrderApi.Infrastructure.DB;

namespace OrderApi.Presentation;

public static class Extensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add database context
        var sqliteConnection = configuration.GetConnectionString("SqliteConnection");
        services.AddDbContext<OrderDbContext>(options => options.UseSqlite(sqliteConnection));

        services.AddSharedServices(configuration);

        services.AddInfrastructureServices();

        services.AddApplicationService(configuration);

        return services;
    }

    public static IApplicationBuilder UseApiPolicies(this IApplicationBuilder app)
    {
        app.UseSharedPolicies();

        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
        {
            var context = serviceScope?.ServiceProvider.GetRequiredService<OrderDbContext>();
            context?.Database.Migrate();
        }

        return app;
    }
}
