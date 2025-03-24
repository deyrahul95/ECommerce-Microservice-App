namespace ECommerce.Shared.Services.Interfaces;

public interface ILoggerService
{
    public void LogError(string message, Exception? exception= null);
    public void LogWarning(string message);
    public void LogInformation(string message);
}
