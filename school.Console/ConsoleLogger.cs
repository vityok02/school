using SchoolManagement.Models;
using static SchoolManagement.TextColors;

namespace SchoolManagement;

public class ConsoleLogger : ILogger
{
    public void LogInfo(string? message = null)
    {
        ChangeToWhite(message!);
    }
    public void LogInfo(int message)
    {
        ChangeToWhite(message);
    }
    public void LogSuccess(string message)
    {
        ChangeToGreen(message);
    }
    public void LogError(string message)
    {
        ChangeToRed(message);
    }
}