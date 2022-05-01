using School;
Address malynivka = new()
{
    Country = "Ukraine",
    City = "Malynivka",
    Street = "Вул. Шкільна 1",
    PostalCode = 22360
};

Employee director = new()
{
    FirstName = "Володимир",
    LastName = "Сиченко"
};

School.School malynivskaSchool = new()
{
    Address = malynivka,
    Name = "Малинівська школа #1",
    OpeningDate = new DateOnly(2002, 1, 1),
    Floors = new List<Floor>(),
    Rooms = new List<Room>(),
    Director = director,
    Employees = new List<Employee>()
};

Floor firstFloor = new()
{
    Number = 1,
};

malynivskaSchool.Floors.Add(firstFloor);

Floor secondFloor = new()
{
    Number = 2,
};
malynivskaSchool.Floors.Add(secondFloor);

Floor thirdFloor = new()
{
    Number = 3,
};
malynivskaSchool.Floors.Add(thirdFloor);

Room mathRoom = new()
{
    Number = 101,
    Type = RoomType.Regular | RoomType.Math,
    Floor = firstFloor
};

Room bioRoom = new()
{
    Number = 202,
    Type = RoomType.Regular | RoomType.Biology,
    Floor = secondFloor
};
Room informRoom = new()
{
    Number = 103,
    Type = RoomType.Regular | RoomType.Informatic,
    Floor = firstFloor
};
Room literatureRoom = new()
{
    Number = 204,
    Type = RoomType.Regular | RoomType.Literature,
    Floor = secondFloor
};
Room gymRoom = new()
{
    Number = 100,
    Type = RoomType.Regular | RoomType.Gym,
    Floor = thirdFloor
};
Room physicsRoom = new()
{
    Number = 203,
    Type = RoomType.Physics,
    Floor = secondFloor
};
Room hallRoom = new()
{
    Number = 200,
    Type = RoomType.Hall,
    Floor = secondFloor
};


malynivskaSchool.Rooms.Add(mathRoom);
malynivskaSchool.Rooms.Add(bioRoom);
malynivskaSchool.Rooms.Add(informRoom);
malynivskaSchool.Rooms.Add(literatureRoom);
malynivskaSchool.Rooms.Add(gymRoom);
malynivskaSchool.Rooms.Add(physicsRoom);
malynivskaSchool.Rooms.Add(hallRoom);

firstFloor.AddRoom(mathRoom);

firstFloor.AddRoom(informRoom);

secondFloor.AddRoom(bioRoom);

secondFloor.AddRoom(literatureRoom);

thirdFloor.AddRoom(gymRoom);

secondFloor.AddRoom(physicsRoom);

secondFloor.AddRoom(hallRoom);

malynivskaSchool.Print();