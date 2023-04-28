using SchoolManagement.Models;
using SchoolManagement.Web.Pages.Rooms;

namespace SchoolManagement.Web.Pages.Floors;

public static class FloorExtension
{
    public static FloorItemDto ToFloorItemDto(this Floor floor)
    {
        Dictionary<int, string> rooms = new();
        rooms.Add(floor.Rooms.Select(r => new object() { Number = r.Number, Type = r.Type.ToString()}));
        return new FloorItemDto(
            floor.Id,
            floor.Number,
            floor.Rooms.Select(r => r.Number);
    }
}
