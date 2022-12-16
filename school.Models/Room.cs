using System.Text.Json.Serialization;

namespace SchoolManagement.Models;

public class Room : BaseEntity
{
    public Room()
    {

    }
    public Room(int number, RoomType type, Floor floor)
    {
        Number = number;
        Type = type;
        Floor = floor;
    }

    public int Number { get; set; }
    public RoomType Type { get; set; }
    public Floor Floor { get; set; }

    public override string ToString()
    {
        return $"Room: {Number}, {Type}";
    }
}
