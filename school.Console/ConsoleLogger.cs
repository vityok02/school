using school.Models;
using static school.TextColors;

namespace school;

public class ConsoleLogger : ILogger
{
    public void LogInfo(string? message = null)
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