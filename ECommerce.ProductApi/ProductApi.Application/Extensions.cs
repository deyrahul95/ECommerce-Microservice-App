using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Services;
using ProductApi.Application.Services.Interfaces;

namespace ProductApi.Application;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
