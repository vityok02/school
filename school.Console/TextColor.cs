﻿namespace school;
public static class TextColors
{
    public static void ChangeToWhite()
    {
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void ChangeToGreen()
    {
        Console.ForegroundColor = ConsoleColor.Green;
    }

    public static void ChangeToRed()
    {
        Console.ForegroundColor = ConsoleColor.Green;
    }

    public static void ChangeColor(string color)
    {
        if(color == "red")
        Console.ForegroundColor = ConsoleColor.Red;
    }
}