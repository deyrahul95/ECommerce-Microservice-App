using System.Text;
using ECommerce.Shared.Middlewares;
using ECommerce.Shared.Services;
using ECommerce.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Shared;

public static class Extensions
{
    public static IServiceCollection AddSharedServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddJWTAuthenticationScheme(configuration);

        services.AddTransient<ILoggerService, LoggerService>();

        return services;
    }

    public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
    {
        // Use global exception
        app.UseMiddleware<GlobalExceptionHandler>();

        // Register middleware to block all outsider API calls
        app.UseMiddleware<ListenToOnlyApiGateway>();

        return app;
    }

    public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration configuration)
    {
        // Add jwt authentication scheme
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("Bearer", options =>
            {
                var key = Encoding.UTF8.GetBytes(configuration.GetSection("Authentication:Key").Value!);
                var issuer = configuration.GetSection("Authentication:Issuer").Value;
                var audience = configuration.GetSection("Authentication:Audience").Value;

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        return services;
    }
}
