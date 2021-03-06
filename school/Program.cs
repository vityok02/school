using SchoolNamespace;
using static SchoolNamespaceMgmnt.ConsoleHelper;

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
    var group = GetValueFromConsole("Enter student group: ");

    Student student = new(firstName, lastName, age, group);
    Context.School?.AddStudent(student);

    Context.School?.Print();
    Console.WriteLine();
}