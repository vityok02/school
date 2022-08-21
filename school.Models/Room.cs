using System.Text.Json.Serialization;

namespace school.Models;

public class Room
{
    public Room(int number, RoomType type, Floor floor)
    {
        Number = number;
        Type = type;
        Floor = floor;
    }

    public int Number { get; set; }
    public RoomType Type { get; set; }
    [JsonIgnore]
    public Floor Floor { get; set; }

    public void Print()
    {
        Console.WriteLine($"Room: {Number}, {Type}");
    }
}
