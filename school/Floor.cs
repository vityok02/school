namespace School
{
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
                room.Print();
            }
        }
    }
}
