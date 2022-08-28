using school;
using school.Data;
using school.Models;
using static school.ConsoleHelper;
using static school.TextColors;

Context Ctx = new();

var filePath = GetFilePath();

ILogger logger = new ConsoleLogger();

SchoolRepository schoolRepository = new(Ctx, filePath, logger);

logger.LogInfo("Welcome to School Management System!");
logger.LogInfo();

while (true)
{
    ShowMenu(Ctx);

    var choice = GetMenuChoice();

    if (!choice.HasValue)
    {
        logger.LogError("Wrong choice");
        continue;
    }

    if (choice == MenuItems.Quit)
    {
        logger.LogInfo("Good Bye!");
        return;
    }

    HandleChoice(choice);
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
            logger.LogError("Unknown choice");
            break;
    }
}

void AddSchool()
{
    var name = GetValueFromConsole("Enter school name: ");
    var address = GetAddress();
    var openingDate = GetDateFromConsole("Enter school opening date: ").ToString();

    School school = new(name, address, openingDate, logger) ;
    schoolRepository.AddSchool(school);

    logger.LogSuccess($"School {school.Name} successfully added");

    logger.LogInfo(school.ToString());
    logger.LogInfo();
}

void SelectSchool()
{
    logger.LogInfo("--------------------");
    var schools = schoolRepository.GetSchools().ToArray();

    if(schools.Length == 0)
    {
        logger.LogInfo("List of schools is empty");
        logger.LogInfo("--------------------");
        logger.LogInfo();
        return;
    }

    while (true)
    {
        for(int i = 0; i < schools.Length; i++)
        {
            logger.LogInfo($"{i}: {schools[i].Name}");
        }
        logger.LogInfo("--------------------");
        var schoolIndex = GetIntValueFromConsole("Choose school: ");
        
        if (schoolIndex < schools.Length)
        {
            schoolRepository.SetCurrentSchool(schools[schoolIndex]);
            break;
        }
        logger.LogError("Please choose correct number from the list above.");
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

void ShowInfo()
{
    logger.LogInfo(Ctx.CurrentSchool?.ToString());
}

void AddFloor()
{
    var floorNumber = GetIntValueFromConsole("Enter floor`s number: ");
    Floor floor = new(floorNumber);

    schoolRepository.AddFloorToCurrentSchool(floor);
    logger.LogSuccess($"{floor.Number} floor successfully added");

    logger.LogInfo(Ctx.CurrentSchool?.ToString());
    logger.LogInfo();
}

void AddRoom()
{
    int floorNumber;//
    Floor floor;//

    while (true)
    {
        floorNumber = GetIntValueFromConsole("Enter floor number: ");
        floor = schoolRepository.GetFloor(floorNumber)!;

        if (floor is null)
        {
            logger.LogError($"Floor {floorNumber} does not exists. Either add new floor or enter correct floor number");
            continue;
        }

        var roomNumber = GetIntValueFromConsole("Enter room number: ");
        var roomType = GetRoomTypeFromConsole("Enter room type");

        schoolRepository.AddRoomToCurrentSchool(new(roomNumber, roomType, floor), floor);
        logger.LogSuccess($"Room with number {roomNumber} successfully added");
        break;
    }

    logger.LogInfo(Ctx.CurrentSchool?.ToString());
    logger.LogInfo();
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
            logger.LogError("Wrong employee type");
        }
    }

    logger.LogInfo(Ctx.CurrentSchool?.ToString());
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
    logger.LogSuccess($"Student {lastName} {firstName} successfully added");

    logger.LogInfo(Ctx.CurrentSchool?.ToString());
    logger.LogInfo();
}

string GetFilePath()
{
    string folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    var fileName = $"{GetValueFromConsole("Enter storage file name: ")}.json";
    return Path.Combine(folder, fileName);
}