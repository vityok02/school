using school.Data;
using school.Models;

namespace school;

public static class ConsoleHelper
{
    static ILogger logger = new ConsoleLogger();
    public static void ShowMenu(AppDbContext dbContext)
    {
        logger.LogInfo("Make your choice");

        Dictionary<MenuItems, string> menuItems = new()
        {
            { MenuItems.CreateSchool, "Create school" },
            { MenuItems.SelectSchool, "Select school" },
            { MenuItems.AddFloor, "Add floor" },
            { MenuItems.AddRoom, "Add room" },
            { MenuItems.AddEmployee, "Add employee" },
            { MenuItems.AddStudent, "Add student" },
            { MenuItems.ShowInfo, "Show all information" },
            { MenuItems.Quit, "Quit" }
        };

        foreach (var item in menuItems)
        {
            logger.LogInfo($"{(int)item.Key}: {item.Value}");
        }
    }

    public static MenuItems? GetMenuChoice()
    {
        return Enum.TryParse<MenuItems>(Console.ReadLine(), out var choice)
            ? choice
            : (MenuItems?)null;
    }

    public static string GetValueFromConsole(string message)
    {
        string? consoleValue;
        while (true)
        {
            logger.LogInfo(message);
            consoleValue = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(consoleValue))
            {
                break;
            }
        }
        return consoleValue;
    }

    public static DateOnly GetDateFromConsole(string message)
    {
        DateOnly openingDate;

        while (true)
        {
            var strValue = GetValueFromConsole(message);

            if (DateOnly.TryParse(strValue, out openingDate))
            {
                break;
            }
            logger.LogInfo($"{strValue} is not correct date format. Try 'MM-DD-YYYY'");
        }
        return openingDate;
    }

    public static int GetIntValueFromConsole(string message)
    {
        int intValue;
        while (true)
        {
            var strValue = GetValueFromConsole(message);
            if (int.TryParse(strValue, out intValue))
            {
                break;
            }
            logger.LogInfo($"{strValue} is not correct number");
        }
        return intValue;
    }

    public static RoomType GetRoomTypeFromConsole(string message)
    {
        RoomType roomType;

        while (true)
        {
            ShowRoomTypes();
            var strValue = GetValueFromConsole(message);

            if (Enum.TryParse(strValue, out roomType))
            {
                return roomType;
            }
            logger.LogInfo($"Incorrect room type: {strValue}");
        }

        void ShowRoomTypes()
        {
            foreach (var type in RoomTypeExt.RoomTypes)
            {
                logger.LogInfo($"{type.Key} - {type.Value}");
            }

            logger.LogInfo("Please choose the room type. If there could be more than one type you combine them by adding numbers. For example: 'Regular' and 'Biology' will be 1 + 4 = 5");
        }
    }
}
