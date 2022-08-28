using school.Models;
using static school.TextColors;

namespace school;

public class ConsoleLogger : ILogger
{
    public void LogInfo(string? message = null)
    {
        Console.WriteLine(message);
    }
    public void LogError(string message)
    {
        ChangeToRed(message);
    }
}