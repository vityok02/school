namespace School;

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
