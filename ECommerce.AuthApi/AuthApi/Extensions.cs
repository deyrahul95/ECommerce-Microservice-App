using AuthApi.DB;
using AuthApi.Users.Configs;
using AuthApi.Users.Repositories;
using AuthApi.Users.Repositories.Interfaces;
using AuthApi.Users.Services;
using AuthApi.Users.Services.Interfaces;
using ECommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace AuthApi;

public static class Extensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Authentication>(configuration.GetSection("Authentication"));

         // Add database context
        var sqliteConnection = configuration.GetConnectionString("SqliteConnection");
        services.AddDbContext<UserDbContext>(options => options.UseSqlite(sqliteConnection));

        services.AddSharedServices(configuration);

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    public static IApplicationBuilder UseApiPolicy(this IApplicationBuilder app)
    {
        app.UseSharedPolicies();
        
        return app;
    }
}
