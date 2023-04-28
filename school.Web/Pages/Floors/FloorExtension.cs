using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Floors;

public static class FloorExtension
{
    public static FloorItemDto ToFloorItemDto(this Floor floor)
    {
        return new FloorItemDto(
            floor.Id,
            floor.Number,
            floor.Rooms.Select(r => r.ToFloorRoomDto()));
    }

    public static FloorRoomDto ToFloorRoomDto(this Room room)
    {
        return new FloorRoomDto(
            room.Number,
            room.Type.ToString());
    }

}
