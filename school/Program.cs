using School;
Address malynivka = new()
{
    Country = "Ukraine",
    City = "Malynivka",
    Street = "Вул. Шкільна 1",
    PostalCode = 22360
};
Student student = new("Ivan", "Ivanov", 17);

School.School malynivskaSchool = new()
{
    Address = malynivka,
    Name = "Малинівська школа #1",
    OpeningDate = new DateOnly(2002, 1, 1),
    //Director = director,
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

firstFloor.AddRoom(mathRoom);

firstFloor.AddRoom(informRoom);

secondFloor.AddRoom(bioRoom);

secondFloor.AddRoom(literatureRoom);

thirdFloor.AddRoom(gymRoom);

secondFloor.AddRoom(physicsRoom);

secondFloor.AddRoom(hallRoom);

Director director = new("Ivan", "Ivanov", 29);
malynivskaSchool.AddEmployee(director);

Teacher teacher1 = new("Petro", "Petrenko", 99);
malynivskaSchool.AddEmployee(teacher1);

Director fakeDirector = new("Fake", "Director", 190);
malynivskaSchool.AddEmployee(fakeDirector);

malynivskaSchool.Print();
