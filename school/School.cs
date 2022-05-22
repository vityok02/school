namespace School
{
    class School
    {
        public Address Address { get; set; }
        public IEnumerable<Room> Rooms 
        { 
            get
            {
                List<Room> allRooms = new List<Room>();
                foreach (Floor floor in Floors)
                {
                    allRooms.AddRange(floor.Rooms);
                }
                return allRooms;
            }
        }
        public DateOnly OpeningDate { get; set; }
        public string Name { get; set; }
        public Employee Director { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public void Print()
        {
            Console.WriteLine($"Name {Name}");
            foreach (Floor floor in Floors)
            {
                floor.Print();
            }

            Console.WriteLine($"Total rooms count: {Rooms.Count()}");
        }
        private readonly List<Floor> _floors;
        public IEnumerable<Floor> Floors => _floors;
        public School()
        {
            _floors = new List<Floor>();
        }
        public void AddFloor(Floor floor)
        {
            _floors.Add(floor);
        }
    }
    class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int PostalCode { get; set; }
    }
    class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    class Room
    {
        public int Number { get; set; }
        public RoomType Type { get; set; }
        public Floor Floor { get; set; }
    }
    class Floor
    {
        public int Number { get; set; }
        private List<Room> _rooms;
        public IEnumerable<Room> Rooms => _rooms;
        public Floor()
        {
            _rooms = new List<Room>();
        }
        public void AddRoom(Room room)
        {
            _rooms.Add(room);
            room.Floor = this;
        }
        public void Print()
        {
            Console.WriteLine($"Floor: {Number} Rooms count: {Rooms.Count()}");
            foreach (Room room in Rooms)
            {
                Console.WriteLine($"Room: {room.Number}, {room.Type}, Floor: {room.Floor.Number}");
            }
        }
    }
    [Flags]
    enum RoomType
    {
        Regular = 1,
        Math = 2,
        Biology = 4,
        Literature = 8,
        Informatic = 16,
        Gym = 32,
        Physics = 64,
        Hall = 128,
    }
}
