using school;
using school.Models;
using school.Data;
using static school.ConsoleHelper;
using static school.TextColors;

ChangeToWhite();

Console.WriteLine("Welcome to School Management System!");
Console.WriteLine();

Context Ctx = new(); //

var filePath = GetFilePath();

SchoolRepository schoolRepository = new(Ctx, filePath); //

while (true)
{
    ShowMenu(Ctx);

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

void AddSchool()
{
    var name = GetValueFromConsole("Enter school name: ");
    var address = GetAddress();
    var openingDate = GetDateFromConsole("Enter school opening date: ").ToString();

    School school = new(name, address, openingDate);
    schoolRepository.AddSchool(school);

    ChangeToGreen();
    Console.WriteLine($"School {school.Name} successfully added");
    ChangeToWhite();

    school.Print();
    Console.WriteLine();
}

void SelectSchool()
{
    Console.WriteLine("--------------------");
    var schools = schoolRepository.GetSchools().ToArray();

    if(schools.Length == 0)
    {
        Console.WriteLine("List of schools is empty");
        Console.WriteLine();
        return;
    }

    while (true)
    {
        for(int i = 0; i < schools.Length; i++)
        {
            Console.WriteLine($"{i}: {schools[i].Name}");
        }
        Console.WriteLine("--------------------");
        var schoolIndex = GetIntValueFromConsole("Choose school: ");
        
        if (schoolIndex < schools.Length)//
        {
            schoolRepository.SetCurrentSchool(schools[schoolIndex]);
            break;
        }
        Console.WriteLine("Please choose correct number from the list above.");
    }
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
            AddSchool();
            break;
        case MenuItems.SelectSchool:
            SelectSchool();
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
    Ctx.CurrentSchool?.Print();
}

void AddFloor()
{
    var floorNumber = GetIntValueFromConsole("Enter floor`s number: ");
    Floor floor = new(floorNumber);

    schoolRepository.AddFloorToCurrentSchool(floor);

    Ctx.CurrentSchool?.Print();
    Console.WriteLine();
}

void AddRoom()
{
    while (true)
    {
        var floorNumber = GetIntValueFromConsole("Enter floor number: ");
        var floor = schoolRepository.GetFloor(floorNumber);

        if (floor is null)
        {
            ChangeToRed();
            Console.WriteLine($"Floor {floorNumber} does not exists. Either add new floor or enter correct floor number");
            ChangeToWhite();
            continue;
        }

        var roomNumber = GetIntValueFromConsole("Enter room number: ");
        var roomType = GetRoomTypeFromConsole("Enter room type");

        schoolRepository.AddRoomToCurrentSchool(new(roomNumber, roomType, floor), floor);
        break;
    }

    Ctx.CurrentSchool?.Print();
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
            schoolRepository.AddEmployeeToCurrentSchool(new Teacher(firstName, lastName, age));
            break;
        }
        else if (type == "D")
        {
            schoolRepository.AddEmployeeToCurrentSchool(new Director(firstName, lastName, age));
            break;
        }
        else
        {
            ChangeToRed();
            Console.WriteLine("Wrong employee type");
            ChangeToWhite();
        }
    }

    Ctx.CurrentSchool?.Print();
    Console.WriteLine();
}

void AddStudent()
{
    var firstName = GetValueFromConsole("Enter student first name: ");
    var lastName = GetValueFromConsole("Enter student last name: ");
    var age = GetIntValueFromConsole("Enter student age: ");
    var group = GetValueFromConsole("Enter student group: ");

    Student student = new(firstName, lastName, age, group);
    schoolRepository.AddStudentToCurrentSchool(student);

    Ctx.CurrentSchool?.Print();
    Console.WriteLine();
}

string GetFilePath()
{
    string folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    var fileName = GetValueFromConsole("Enter storage file name: ");
    return Path.Combine(folder, fileName);
}