using System.Text;
using System.Text.Json.Serialization;

namespace school.Models;

public class Floor
{
    public int Number { get; set; }
    private readonly List<Room> _rooms = new();
    public IEnumerable<Room> Rooms => _rooms;
    public Floor(int number)
    {
        Number = number;
    }
    private ILogger _logger;

    public void AddRoom(Room room)
    {
        if (room.Number < 0)
        {
            _logger.LogError("room number must be greater than 0");
            return;
        }

        for (int i = 0; i < _rooms.Count; i++)
        {
            Room r = _rooms[i];
            if (r.Number == room.Number)
            {
                _logger.LogError("This room number already exists");
                return;
            }
        }

        _rooms.Add(room);
        room.Floor = this;
    }

    [JsonConstructor]
    public Floor(int number, IEnumerable<Room> rooms)
    {
        Number = number;
        _rooms = rooms.ToList();
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