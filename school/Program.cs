using SchoolNamespace;

while (true)
{
    ShowMenu(Context.School);

    var choice = GetMenuChoice();

    if (!choice.HasValue)
    {
        Console.WriteLine("Wrong choice");
        continue;
    }

    if (choice == MenuItems.Quit)
    {
        Console.WriteLine("Good Bye!");
        return;
    }

    HandleChoice(choice);
}

static MenuItems? GetMenuChoice()
{
    return Enum.TryParse<MenuItems>(Console.ReadLine(), out var choice)
        ? choice
        : (MenuItems?)null;
}


void ShowMenu(School school)
{
    Console.WriteLine("Make your choice");

    Dictionary<MenuItems, string> menuItems = new()
    {
        {MenuItems.CreateSchool, "Create school" },
        {MenuItems.AddFloor, "Add floor" },
        {MenuItems.AddRoom, "Add room" },
        {MenuItems.AddEmployee, "Add employee" },
        {MenuItems.AddStudent, "Add student" },
        {MenuItems.ShowInfo, "Show all information" },
        {MenuItems.Quit, "Quit" }
    };

    foreach (var item in menuItems)
    {
        if (item.Key != MenuItems.CreateSchool || school is null)
        {
            Console.WriteLine($"{(int)item.Key}: {item.Value}");
        }
    }
}

static string GetValueFromConsole(string message)
{
    string? consoleValue;
    while(true)
    {
        Console.WriteLine(message);
        consoleValue = Console.ReadLine();

        if(!string.IsNullOrWhiteSpace(consoleValue))
        {
            break;
        }
    }
    return consoleValue;
}

void CreateSchool()
{
    var name = GetValueFromConsole("Enter school name: ");
    var address = GetAddress();
    var openingDate = GetDateFromConsole("Enter school opening date: ");

    School school = new(name, address, openingDate);

    Context.School = school;

    Console.WriteLine($"School {school.Name} successfully added");
    school.Print();
    Console.WriteLine();

}

static DateOnly GetDateFromConsole(string message)
{
    DateOnly openingDate;
    while (true)
    {
        var strValue = GetValueFromConsole(message);

        if (DateOnly.TryParse(strValue, out openingDate))
        {
            break;
        }
        Console.WriteLine($"{strValue} is not correct date format. Try 'YYYY-MM-DD'");
    } 
    return openingDate;
}

Address GetAddress()
{
    var country = GetValueFromConsole("Enter school country: ");
    var city = GetValueFromConsole("Enter school city or town: ");
    var street = GetValueFromConsole("Enter school street: ");
    var postalCode = GetIntValueFromConsole("Enter school postal code: ");

    return new(country, city, street, postalCode);
}

static int GetIntValueFromConsole(string message)
{
    int intValue;
    while (true)
    {
        var strValue = GetValueFromConsole(message);
        if (int.TryParse(strValue, out intValue))
        {
            break;
        }
        Console.WriteLine($"{strValue} is not correct number");
    }
    return intValue;
}

void HandleChoice(MenuItems? choice)
{
    switch (choice)
    {
        case MenuItems.CreateSchool:
            CreateSchool();
            break;
        case MenuItems.AddFloor:
            AddFloor();
            break;
        case MenuItems.AddRoom:
            AddRoom();
            break;
        case MenuItems.AddEmployee:
            AddEmployee();
            break;
        case MenuItems.AddStudent:
            AddStudent();
            break;
        case MenuItems.ShowInfo:
            ShowInfo();
            break;
        default:
            Console.WriteLine("Unknown choice");
            break;

    }
}

void ShowInfo()
{
    Context.School?.Print();
}

void AddFloor()
{
    var floorNumber = GetIntValueFromConsole("Enter floor`s number");
    Floor floor = new(floorNumber);

    Context.School?.AddFloor(floor);
    Context.School?.Print();
    Console.WriteLine();
}

void AddRoom()
{
    while(true)
    {
        var floorNumber = GetIntValueFromConsole("Enter floor number");
        var floor = Context.School?.Floors.FirstOrDefault(f => f.Number == floorNumber);

        if(floor is null)
        {
            Console.WriteLine($"Floor {floorNumber} does not exists. Either add new floor or enter correct floor number");
            continue;
        }

        var roomNumber = GetIntValueFromConsole("Enter room number: ");
        var roomType = GetRoomTypeFromConsole("Enter room type");

        floor.AddRoom(new(roomNumber, roomType, floor));
        break;
    }

    Context.School?.Print();
    Console.WriteLine();
}

static RoomType GetRoomTypeFromConsole(string message)
{
    RoomType roomType;

    while (true)
    {
        ShowRoomTypes();
        var strValue = GetValueFromConsole(message);

        if (Enum.TryParse<RoomType>(strValue, out roomType))
        {
            return roomType;
        }
        Console.WriteLine($"Incorrect room type: {strValue}");
    }

    void ShowRoomTypes()
    {
        foreach (var type in RoomTypeExt.RoomTypes)
        {
            Console.WriteLine($"{type.Key} - {type.Value}");
        }

        Console.WriteLine("Please choose the room type. If there could be more than one type you combine them by adding numbers. For example: 'Regular' and 'Biology' will be 1 + 4 = 5");
    }
}

void AddEmployee()
{
    var firstName = GetValueFromConsole("Enter employee first name: ");
    var lastName = GetValueFromConsole("Enter employee last name: ");
    var age = GetIntValueFromConsole("Enter employee age: ");

    while (true)
    {
        var type = GetValueFromConsole("If director enter (d), if teacher enter (t): ").ToUpperInvariant();

        if (type == "T")
        {
            Context.School?.AddTeacher(firstName, lastName, age);
            break;
        }
        else if (type == "D")
        {
            Context.School?.AddDirector(firstName, lastName, age);
            break;
        }
        else
        {
            Console.WriteLine("Wrong employee type");
        }
    }

    Context.School?.Print();
    Console.WriteLine();
}

void AddStudent()
{
    var firstName = GetValueFromConsole("Enter student first name: ");
    var lastName = GetValueFromConsole("Enter student last name: ");
    var age = GetIntValueFromConsole("Enter student age: ");

    Student student = new(firstName, lastName, age);
    Context.School?.AddStudent(student);

    Context.School?.Print();
    Console.WriteLine();
}