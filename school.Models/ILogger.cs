namespace school.Models;

public interface ILogger
{
    void LogInfo(string? message = null);
    void LogError(string message);
}