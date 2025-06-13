using ApiGateway.Middlewares;
using ECommerce.Shared;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services
    .AddOcelot()
    .AddCacheManager(x => x.WithDictionaryHandle())
    .AddConsul();

builder.Services.AddJWTAuthenticationScheme(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors();

app.UseMiddleware<AddSignatureToRequest>();

await app.UseOcelot();

await app.RunAsync();