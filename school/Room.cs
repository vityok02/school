namespace School
{
    class Room
    {
        public int Number { get; set; }
        public RoomType Type { get; set; }
        public Floor Floor { get; set; }
        public void Print()
        {
            Console.WriteLine($"Room: {Number}, {Type}, Floor: {Floor.Number}");
        }
    }
}
