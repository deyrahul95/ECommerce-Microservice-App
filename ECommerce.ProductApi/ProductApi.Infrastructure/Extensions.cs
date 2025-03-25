using Microsoft.Extensions.DependencyInjection;
using ProductApi.Infrastructure.Repositories;
using ProductApi.Infrastructure.Repositories.Interfaces;

namespace ProductApi.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        
        return services;
    }
}
