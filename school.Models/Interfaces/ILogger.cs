namespace SchoolManagement.Models.Interfaces;

public interface ILogger
{
    void LogInfo(string? message = null);
    void LogInfo(int message);
    void LogSuccess(string message);
    void LogError(string message);
}