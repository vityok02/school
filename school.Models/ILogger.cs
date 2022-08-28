namespace school.Models;

public interface ILogger
{
    void LogInfo(string? message = null);
    void LogSuccess(string message);
    void LogError(string message);
}