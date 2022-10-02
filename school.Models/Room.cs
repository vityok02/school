using System.Text.Json.Serialization;

namespace school.Models;

public class Room : BaseEntity
{
    public Room(int id, int number, RoomType type, Floor floor)
    {
        Id = id;
        Number = number;
        Type = type;
        Floor = floor;
    }

    public int Number { get; set; }
    public RoomType Type { get; set; }
    [JsonIgnore]
    public Floor Floor { get; set; }

    public override string ToString()
    {
        return $"Room: {Number}, {Type}";
    }
}
