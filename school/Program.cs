using School;
Address malynivka = new()
{
    Country = "Ukraine",
    City = "Malynivka",
    Street = "Вул. Шкільна 1",
    PostalCode = 22360
};

School.School malynivskaSchool = new()
{
    Address = malynivka,
    Name = "Малинівська школа #1",
    OpeningDate = new DateOnly(2002, 1, 1),
};
Floor firstFloor = new()
{
    Number = 1,
};

malynivskaSchool.AddFloor(firstFloor);

Floor secondFloor = new()
{
    Number = 2,
};
malynivskaSchool.AddFloor(secondFloor);

Floor thirdFloor = new()
{
    Number = 3,
};
malynivskaSchool.AddFloor(thirdFloor);
Floor fourthFloor = new()
{
    Number = 4,
};
malynivskaSchool.AddFloor(fourthFloor);


Room mathRoom = new()
{
    Number = 101,
    Type = RoomType.Regular | RoomType.Math,
    Floor = firstFloor
};

Room bioRoom = new()
{
    Number = 202,
    Type = RoomType.Regular | RoomType.Biology
};
Room informRoom = new()
{
    Number = 103,
    Type = RoomType.Regular | RoomType.Informatic
};
Room literatureRoom = new()
{
    Number = 204,
    Type = RoomType.Regular | RoomType.Literature
};
Room gymRoom = new()
{
    Number = 100,
    Type = RoomType.Regular | RoomType.Gym
};
Room physicsRoom = new()
{
    Number = 203,
    Type = RoomType.Physics
};
Room hallRoom = new()
{
    Number = 200,
    Type = RoomType.Hall
};
Room workshop = new()
{
    Number = 200,
    Type = RoomType.Workshop
};

//try
//{
//    firstFloor.AddRoom(mathRoom);
//    firstFloor.AddRoom(informRoom);
//    secondFloor.AddRoom(bioRoom);
//    secondFloor.AddRoom(literatureRoom);
//    thirdFloor.AddRoom(gymRoom);
//    secondFloor.AddRoom(physicsRoom);
//    secondFloor.AddRoom(hallRoom);
//    firstFloor.AddRoom(workshop);

//    malynivskaSchool.AddDirector("Ivan", "Ivanov", 29);
//    malynivskaSchool.AddDirector("Fake", "Director", 190);
//    malynivskaSchool.AddTeacher("Petro", "Petrenko", 99);
//    malynivskaSchool.AddTeacher("Vladymir", "Zelensky", 24);
//    malynivskaSchool.AddTeacher("Vladymir", "Zelensky", 24);
//    malynivskaSchool.AddTeacher("Vladymir", "Zelensky", 45);
//    malynivskaSchool.AddTeacher("Teacher", "Young", 5);
//    malynivskaSchool.AddTeacher("Teacher", "Old", 99);
//    malynivskaSchool.AddTeacher("", "", 0);
//    malynivskaSchool.Print();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

PrintMenu();
string choice = GetChoice();

if (choice == "q")
{
    Console.WriteLine("You have exited the program");
    return;
}

Action(choice);

void PrintMenu()
{
    Console.WriteLine("===Hello!===");
    Console.WriteLine("=Enter the choice");
    Console.WriteLine("=Create school\t 's'");
    Console.WriteLine("=Add floor\t 'f'");
    Console.WriteLine("=Add room\t 'r'");
    Console.WriteLine("=Add employee\t 'e'");
    Console.WriteLine("=Quit\t\t 'q'");
}
string GetChoice()
{
    string choice = Console.ReadLine();
    return choice;
}
void Action(string choice)
{
    if (choice == "s")
    {
    }
    if (choice == "f")
    {
        AddFloor();
    }
    if (choice == "r")
    {

    }
    if (choice == "e")
    {
        ChoiceEmployee();
    }
    else
    {
        Console.WriteLine("Error, you have not selected an action");
    }
}
void ChoiceEmployee()
{
    Console.WriteLine("Select an employee type");
    Console.WriteLine("Director 'd'");
    Console.WriteLine("Teacher 't'");
    string emp = Console.ReadLine();
    if (choice == "d")
    {

    }
    if (choice == "t")
    {

    }
}
void AddFloor()
{
    for (int i = 0; ; i++)
    {
        Console.WriteLine("Enter the floor`s number");
        Floor floor = new()
        {
            Number = Convert.ToInt32(Console.ReadLine())
        };
        Console.WriteLine($"{floor.Number} floor was added.");
        Console.WriteLine("Continue?");
        Console.WriteLine("Yes 'y'");
        Console.WriteLine("No 'q'");
        string floorChoice = Console.ReadLine();
        if (floorChoice == "y")
        {
            continue;
        }
        if (floorChoice == "q")
        {
            return;
        }
    }
}
