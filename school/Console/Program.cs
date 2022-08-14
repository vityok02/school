using school;
using school.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using static school.ConsoleHelper;

Console.ForegroundColor = ConsoleColor.White;
OpenJson();

while (true)
{
    ShowMenu(Context.CurrentSchool!);

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

void CreateSchool()
{
    var name = GetValueFromConsole("Enter school name: ");
    var address = GetAddress();
    var openingDate = GetDateFromConsole("Enter school opening date: ").ToString();

    School school = new(name, address, openingDate);

    SaveSchool();

    Context.CurrentSchool = school;

    Console.WriteLine($"School {school.Name} successfully added");
    school.Print();
    Console.WriteLine();
}

Address GetAddress()
{
    var country = GetValueFromConsole("Enter school country: ");
    var city = GetValueFromConsole("Enter school city or town: ");
    var street = GetValueFromConsole("Enter school street: ");
    var postalCode = GetIntValueFromConsole("Enter school postal code: ");

    return new(country, city, street, postalCode);
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
    Context.CurrentSchool?.Print();
}

void AddFloor()
{
    var floorNumber = GetIntValueFromConsole("Enter floor`s number: ");
    Floor floor = new(floorNumber);
    Context.CurrentSchool?.AddFloor(floor);

    SaveSchool();

    Context.CurrentSchool?.Print();
    Console.WriteLine();
}

void AddRoom()
{
    while (true)
    {
        var floorNumber = GetIntValueFromConsole("Enter floor number: ");
        var floor = Context.CurrentSchool?.Floors.FirstOrDefault(f => f.Number == floorNumber);

        if (floor is null)
        {
            Console.WriteLine($"Floor {floorNumber} does not exists. Either add new floor or enter correct floor number");
            continue;
        }

        var roomNumber = GetIntValueFromConsole("Enter room number: ");
        var roomType = GetRoomTypeFromConsole("Enter room type");

        floor.AddRoom(new(roomNumber, roomType, floor));

        SaveSchool();
        break;
    }

    Context.CurrentSchool?.Print();
    Console.WriteLine();
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
            Context.CurrentSchool?.AddTeacher(firstName, lastName, age);
            SaveSchool();
            break;
        }
        else if (type == "D")
        {
            Context.CurrentSchool?.AddDirector(firstName, lastName, age);

            SaveSchool();
            break;
        }
        else
        {
            Console.WriteLine("Wrong employee type");
        }
    }

    Context.CurrentSchool?.Print();
    Console.WriteLine();
}

void AddStudent()
{
    var firstName = GetValueFromConsole("Enter student first name: ");
    var lastName = GetValueFromConsole("Enter student last name: ");
    var age = GetIntValueFromConsole("Enter student age: ");
    var group = GetValueFromConsole("Enter student group: ");

    Student student = new(firstName, lastName, age, group);
    Context.CurrentSchool?.AddStudent(student);

    SaveSchool();

    Context.CurrentSchool?.Print();
    Console.WriteLine();
}

string GetFilePath()
{
    string folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    if (Context.FileName is null)
    {
        var fileName = $"{GetValueFromConsole("Enter the name of file: ")}.json";
        Context.FileName = fileName;
    }
    return Path.Combine(folder, Context.FileName);
}

void SaveSchool()
{
    string jsonString = JsonSerializer.Serialize(Context.Schools, new JsonSerializerOptions
    {
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    });

    var filePath = GetFilePath();

    File.WriteAllText(filePath, jsonString);
}

void OpenJson()
{
    var filePath = GetFilePath();
    if (File.Exists(filePath))
    {
        string schools = File.ReadAllText(filePath);
        Context.Schools = JsonSerializer.Deserialize<IEnumerable<School>>(schools);

        Console.Clear();
        Console.WriteLine($"File {Context.FileName} has been opened");
        Console.WriteLine();
    }
    else
    {
        Console.Clear();
        File.WriteAllText(filePath, Context.FileName);
        Console.WriteLine($"File {Context.FileName} has been created");
        Console.WriteLine();

        CreateSchool();
    }
}