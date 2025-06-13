using ProductApi.Presentation;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddServiceDiscovery(option => option.UseConsul());

builder.Services.AddApiServices(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseApiPolicies();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();