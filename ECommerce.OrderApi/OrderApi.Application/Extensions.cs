using ECommerce.Shared.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Application.Services;
using OrderApi.Application.Services.Interfaces;
using Polly;
using Polly.Retry;

namespace OrderApi.Application;

public static class Extensions
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        var baseAddress = configuration.GetSection("ApiGateway:BaseAddress").Value;

        // Register http client service
        services.AddHttpClient<IHttpService, HttpService>(option =>
        {
            option.BaseAddress = new Uri(baseAddress!);
            option.Timeout = TimeSpan.FromSeconds(1);
        });

        var loggerService = services.BuildServiceProvider().GetRequiredService<ILoggerService>();

        // Create retry strategy
        var retryStrategy = new RetryStrategyOptions()
        {
            ShouldHandle = new PredicateBuilder().Handle<TaskCanceledException>(),
            BackoffType = DelayBackoffType.Constant,
            UseJitter = true,
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromMicroseconds(500),
            OnRetry = args =>
            {
                var message = $"OnRetry, Attempt: {args.AttemptNumber} Outcome: {args.Outcome}";
                loggerService.LogError(message);
                return ValueTask.CompletedTask;
            }
        };

        services.AddResiliencePipeline("order-retry-pipeline", builder =>
        {
            builder.AddRetry(retryStrategy);
        });

        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
