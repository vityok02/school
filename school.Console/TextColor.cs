namespace school;
public static class TextColors
{
    public static void ChangeToWhite()
    {
        System.Console.ForegroundColor = ConsoleColor.White;
    }

    public static void ChangeToGreen()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void ChangeToRed(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }
}