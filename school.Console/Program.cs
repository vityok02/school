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


//SchoolRepository schoolRepository = new(Ctx, filePath, logger);

logger.LogInfo("Welcome to School Management System!");
logger.LogInfo();

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

    School school = new(name, address, DateTime.Parse(openingDate), logger);
    schoolRepository.Add(school);
    dbContext.CurrentSchool = school;

    logger.LogSuccess($"School {school.Name} successfully added");

    logger.LogInfo(school.ToString());
    logger.LogInfo();
}

void SelectSchool()
{
    logger.LogInfo("--------------------");

    var schoolRepository = new Repository<School>(dbContext);

    var schools = schoolRepository.GetAll();

    if (schools.Count() == 0)
    {
        logger.LogInfo("List of schools is empty");
        logger.LogInfo("--------------------");
        logger.LogInfo();
        return;
    }

    while (true)
    {
        foreach (School school in schools)
        {
            logger.LogInfo($"{school.Id}: {school.Name}");
        }
        logger.LogInfo("--------------------");
        var schoolId = GetIntValueFromConsole("Choose school: ");

        if (schoolId < schools.Count())
        {
            //schoolRepository.SetCurrentSchool(schoolId);
            dbContext.CurrentSchool = schools.Where(s => s.Id == schoolId).SingleOrDefault();
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
    //Repository<Floor> floorRepository = new(dbContext);
    //Repository<School> repository = new(dbContext);
    //var currentSchool = repository.GetAll(s => s.Id == dbContext.CurrentSchool.Id).SingleOrDefault();

    var currentSchool = dbContext.Schools
        .Where(s => s.Id == dbContext.CurrentSchool.Id)
        .Include(s => s.Floors)
        .SingleOrDefault();

    var floorNumber = GetIntValueFromConsole("Enter floor`s number: ");
    Floor floor = new(floorNumber);

    foreach (Floor f in currentSchool.Floors)
    {
        if (f.Number == floor.Number)
        {
            logger.LogError($"Floor {floor.Number} already exists");
            return;
        }
    }

    currentSchool.Floors.Add(floor);

    //floor.School = dbContext.CurrentSchool; //

    dbContext.SaveChanges();

    logger.LogInfo(Ctx.CurrentSchool?.ToString());
    logger.LogInfo();
}

void AddRoom()
{
    while (true)
    {
        var floorNumber = GetIntValueFromConsole("Enter floor number: ");
        //var floor = schoolRepository.GetFloor(floorNumber)!;
        //var floor = dbContext.CurrentSchool?.Floors.Where(f => f.Number == floorNumber).FirstOrDefault();

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

        roomRepository.Add(new(roomNumber, roomType, floor));
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
        var currentSchool = dbContext.Schools
            .Where(s => s.Id == dbContext.CurrentSchool.Id)
            .SingleOrDefault();
        if (type == "T")
        {
            currentSchool.Employees.Add(new Teacher(firstName, lastName, age));
            break;
        }
        if (type == "D")
        {
            currentSchool.Director.Add(new Director(firstName, lastName, age));
            dbContext.SaveChanges();
            break;
        }
        //else
        //{
        //    logger.LogError("Wrong employee type");
        //}
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

    Student student = new(firstName, lastName, age, group);
    currentSchool.Students.Add(student);

    dbContext.SaveChanges();

    logger.LogSuccess($"Student {lastName} {firstName} successfully added");

    logger.LogInfo(Ctx.CurrentSchool?.ToString());
    logger.LogInfo();
}