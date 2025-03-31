using Microsoft.Extensions.DependencyInjection;
using OrderApi.Infrastructure.Repositories;
using OrderApi.Infrastructure.Repositories.Interfaces;

namespace OrderApi.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
