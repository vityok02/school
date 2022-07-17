namespace SchoolNamespace;

public class Floor
{
    public int Number { get; set; }
    private readonly List<Room> _rooms = new();
    public IEnumerable<Room> Rooms => _rooms;
    public Floor(int number)
    {
        Number = number;
    }
    public void AddRoom(Room room)
    {
        if (room.Number < 0)
        {
            Console.WriteLine("room number must be greater than 0");
            return;
        }

        for (int i = 0; i < _rooms.Count; i++)
        {
            Room r = _rooms[i];
            if (r.Number == room.Number)
            {
                Console.WriteLine("This room number already exists");
                return;
            }
        }

        _rooms.Add(room);
        room.Floor = this;
    }
    public void Print()
    {
        Console.WriteLine($"Floor: {Number} Rooms count: {Rooms.Count()}");
        foreach (Room room in Rooms)
        {
            room.Print();
        }
    }
}
