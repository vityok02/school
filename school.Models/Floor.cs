using System.Text;

namespace SchoolManagement.Models;

public class Floor : BaseEntity
{
    public int Number { get; set; }
    public ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
    public School School { get; set; }
    public int SchoolId { get; set; }

    public Floor()
    {
    }

    public Floor(int number)
    {
        Number = number;
    }

    public (bool Valid, string? Error) AddRoom(Room room)
    {
        if (room.Number < 0)
        {
            return (false, "room number must be greater than 0");
        }

        if (Rooms.Any(r => r.Number == room.Number))
        {
            return (false, "This room number already exists");
        }

        Rooms.Add(room);
        return (true, null);
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine($"Floor: {Number} Rooms count: {Rooms.Count()}");

        foreach (var room in Rooms)
        {
            sb.AppendLine(room.ToString());
        }

        return sb.ToString();
    }
}