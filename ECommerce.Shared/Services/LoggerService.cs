using ECommerce.Shared.Services.Interfaces;
using Serilog;

namespace ECommerce.Shared.Services;

public class LoggerService : ILoggerService
{
    public LoggerService()
    {
        Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.Console()
           .WriteTo.File("logs/log-*.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();
    }
    public void LogError(string message, Exception? exception = null)
    {
        if (exception == null)
        {
            Log.Error(message);
        }
        else
        {
            Log.Error($"{message} Exception: {exception.Message}");
        }
    }

    public void LogInformation(string message)
    {
        Log.Information(message);
    }

    public void LogWarning(string message)
    {
        Log.Warning(message);
    }
}
