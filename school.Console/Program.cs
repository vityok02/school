using Microsoft.EntityFrameworkCore;
using SchoolManagement;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using static SchoolManagement.ConsoleHelper;

string dataSource = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=SchoolDb";

AppDbContext dbContext = new(new DbContextOptions<AppDbContext>());

ILogger logger = new ConsoleLogger();

var schoolRepository = new Repository<School>(dbContext);

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
        case MenuItems.ShowInfo:
            ShowInfo();
            break;
        default:
            logger.LogError("Unknown choice");
            break;
    }
}

void ShowInfo()
{
    ShowItems();

    var choice = GetItemChoice();
    switch(choice)
    {
        case InfoItems.All:
            ShowAll();
            break;
        case InfoItems.School:
            ShowSchool();
            break;
        case InfoItems.Floors:
            ShowFloors();
            break;
        case InfoItems.Employees:
            ShowEmployees();
            break;
        case InfoItems.Students:
            ShowStudents();
            break;
        default:
            logger.LogError("Unknown choice");
            break;
    }
    void ShowAll()
    {
        var currentSchool = dbContext.Schools
        .Include(s => s.Floors)
        .Include(s => s.Rooms)
        .Include(s => s.Employees)
        .Include(s => s.Students)
        .Include(s => s.Address)
        .Where(s => s.Id == dbContext.CurrentSchool.Id)
        .SingleOrDefault();

        logger.LogInfo(currentSchool!.ToString());
    }
    void ShowSchool()
    {
        var currentSchool = dbContext.Schools
        .Include(s => s.Address)
        .Where(s => s.Id == dbContext.CurrentSchool.Id)
        .SingleOrDefault();

        logger.LogInfo($"Name: {currentSchool!.Name}");
        logger.LogInfo($"Country: {currentSchool.Address.Country}");
        logger.LogInfo($"City(town): {currentSchool.Address.City}");
        logger.LogInfo($"Street: {currentSchool.Address.Street}");
        logger.LogInfo($"Postal code: {currentSchool.Address.PostalCode}");
        logger.LogInfo($"Opening date: {currentSchool.OpeningDate}");
    }
    void ShowFloors()
    {
        var floors = dbContext.Schools
            .Include(f => f.Floors)
            .ThenInclude(f => f.Rooms)
            .Where(s => s.Id == dbContext.CurrentSchool.Id)
            .SingleOrDefault();

        foreach(var floor in floors!.Floors)
        {
            logger.LogInfo(floor.ToString());
        }
    }
    void ShowEmployees()
    {
        var employees = dbContext.Schools
            .Include(e => e.Employees)
            .Where(s => s.Id == dbContext.CurrentSchool.Id)
            .SingleOrDefault();

        foreach (var emp in employees!.Employees)
        {
            logger.LogInfo(emp.ToString());
        }
    }
    void ShowStudents()
    {
        var students = dbContext.Schools
            .Include(f => f.Students)
            .Where(s => s.Id == dbContext.CurrentSchool.Id)
            .SingleOrDefault();

        foreach (var student in students!.Students)
        {
            logger.LogInfo(student.ToString());
        }
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

    logger.LogInfo();
}

void AddRoom()
{
    while (true)
    {
        var floorNumber = GetIntValueFromConsole("Enter floor number: ");

        var currentFloor = dbContext.Floors                                                 
            .Where(f => f.SchoolId == dbContext.CurrentSchool.Id && f.Number == floorNumber)
            .Include(f => f.Rooms)                                                          
            .SingleOrDefault();                                                             

        if (currentFloor is null)
        {
            logger.LogError($"Floor {floorNumber} does not exists. Either add new floor or enter correct floor number");
            continue;
        }

        var roomNumber = GetIntValueFromConsole("Enter room number: ");
        var roomType = GetRoomTypeFromConsole("Enter room type");

        var (valid, error) = currentFloor.AddRoom(new Room(roomNumber, roomType));
        if(!valid)
        {
            logger.LogError(error!);
            return;
        }

        dbContext.SaveChanges();
        logger.LogSuccess($"Room {roomNumber} successfully added");
        break;
    }

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

        Employee? employee = null;

        if (type == "T")
        {
            employee = new Teacher(firstName, lastName, age);
        }
        else if (type == "D")
        {
            employee = new Director(firstName, lastName, age);
        }
        else
        {
            logger.LogError("Wrong employee type");
            continue;
        }

        var (valid, error) = currentSchool!.AddEmployee(employee);
        if (!valid)
        {
            logger.LogError(error!);
            continue;
        }

        dbContext.SaveChanges();

        logger.LogSuccess($"Employee {firstName} {lastName} successfully added");
        break;
    }

    logger.LogInfo();
}

void AddStudent()
{
    var firstName = GetValueFromConsole("Enter student first name: ");
    var lastName = GetValueFromConsole("Enter student last name: ");
    var age = GetIntValueFromConsole("Enter student age: ");
    var group = GetValueFromConsole("Enter student group: ");

    var currentSchool = dbContext.Schools
        .Include(t => t.Students)
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