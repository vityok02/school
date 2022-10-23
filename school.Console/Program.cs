using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using school;
using school.Data;
using school.Models;
using static school.ConsoleHelper;

string dataSource = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=SchoolDb";

AppDbContext dbContext = new(dataSource);

Context Ctx = new();

ILogger logger = new ConsoleLogger();

Repository<School> schoolRepository = new(dbContext);

logger.LogInfo("Welcome to School Management System!");
logger.LogInfo();

SelectSchool();

while (true)
{
    ShowMenu(dbContext);

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
        default:
            logger.LogError("Unknown choice");
            break;
    }
}

void SelectSchool()
{
    logger.LogInfo("--------------------");

    var schoolRepository = new Repository<School>(dbContext);

    var schools = schoolRepository.GetAll();

    while (true)
    {
        foreach (School school in schools)
        {
            logger.LogInfo($"{school.Id}: {school.Name}");
        }
        var lastIndex = schools.Count() + 1;
        logger.LogInfo($"{lastIndex}: Add School");
        logger.LogInfo("--------------------");
        var schoolId = GetIntValueFromConsole("Choose school: ");

        if (schoolId <= schools.Count())
        {
            dbContext.CurrentSchool = schools.Where(s => s.Id == schoolId).SingleOrDefault()!;
            break;
        }
        if (schoolId == lastIndex)
        {
            AddSchool();
            return;
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

void AddSchool()
{
    var name = GetValueFromConsole("Enter school name: ");
    var address = GetAddress();
    var openingDate = GetDateFromConsole("Enter school opening date: ").ToString();

    School school = new(name, address, DateTime.Parse(openingDate));
    schoolRepository.Add(school);
    dbContext.CurrentSchool = school;

    logger.LogSuccess($"School {school.Name} successfully added");

    logger.LogInfo(school.ToString());
    logger.LogInfo();
}

void AddFloor()
{
    var currentSchool = dbContext.Schools
        .Where(s => s.Id == dbContext.CurrentSchool.Id)
        .Include(s => s.Floors)
        .SingleOrDefault();

    var floorNumber = GetIntValueFromConsole("Enter floor`s number: ");
    Floor floor = new(floorNumber);

    var (valid, error) = currentSchool!.AddFloor(floor);
    if (!valid)
    {
        logger.LogError(error!);
        return;
    }

    dbContext.SaveChanges();

    logger.LogSuccess($"{floor.Number} floor successfully added");

    logger.LogInfo(Ctx.CurrentSchool?.ToString());
    logger.LogInfo();
}

void AddRoom()
{
    while (true)
    {
        var floorNumber = GetIntValueFromConsole("Enter floor number: ");

        Repository<Floor> floorRepository = new(dbContext);
        var floor = floorRepository.GetAll(f => f.SchoolId == dbContext.CurrentSchool.Id && f.Number == floorNumber)
            .SingleOrDefault();

        if (floor is null)
        {
            logger.LogError($"Floor {floorNumber} does not exists. Either add new floor or enter correct floor number");
            continue;
        }

        var roomNumber = GetIntValueFromConsole("Enter room number: ");
        var roomType = GetRoomTypeFromConsole("Enter room type");

        Repository<Room> roomRepository = new(dbContext);

        var (valid, error) = floor.AddRoom(new Room(roomNumber, roomType, floor));
        if(!valid)
        {
            logger.LogError(error!);
            return;
        }

        logger.LogSuccess($"Room {roomNumber} successfully added");
        break;
    }

    logger.LogInfo(Ctx.CurrentSchool?.ToString());
    logger.LogInfo();
}

void AddEmployee()
{
    var currentSchool = dbContext.Schools
        .Include(t => t.Employees)
        .Where(s => s.Id == dbContext.CurrentSchool.Id)
        .SingleOrDefault();

    var firstName = GetValueFromConsole("Enter employee first name: ");
    var lastName = GetValueFromConsole("Enter employee last name: ");
    var age = GetIntValueFromConsole("Enter employee age: ");

    while (true)
    {
        var type = GetValueFromConsole("If director enter (d), if teacher enter (t): ").ToUpperInvariant();
        if (type == "T")
        {
            var (valid, error) = currentSchool!.AddEmployee(new Teacher(firstName, lastName, age));
            if (!valid)
            {
                logger.LogError(error!);
                return;
            }

            dbContext.SaveChanges();
            break;
        }
        else if (type == "D")
        {
            var (valid, error) = currentSchool!.AddEmployee(new Director(firstName, lastName, age));
            if(!valid)
            {
                logger.LogError(error!);
                return;
            }
            dbContext.SaveChanges();
            break;
        }
        else
        {
            logger.LogError("Wrong employee type");
        }
        logger.LogSuccess($"Employee {firstName} {lastName} successfully added");
    }

    logger.LogInfo(Ctx.CurrentSchool?.ToString());
    logger.LogInfo();
}

void AddStudent()
{
    var firstName = GetValueFromConsole("Enter student first name: ");
    var lastName = GetValueFromConsole("Enter student last name: ");
    var age = GetIntValueFromConsole("Enter student age: ");
    var group = GetValueFromConsole("Enter student group: ");

    var currentSchool = dbContext.Schools
        .Where(s => s.Id == dbContext.CurrentSchool.Id)
        .SingleOrDefault();

    var (valid, error) = currentSchool!.AddStudent(new Student(firstName, lastName, age, group));
    if(!valid)
    {
        logger.LogError(error!);
        return;
    }
    logger.LogSuccess($"Student {firstName} {lastName} successfully added to group {group}");
    dbContext.SaveChanges();

    
}