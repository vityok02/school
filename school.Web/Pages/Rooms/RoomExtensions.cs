using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Rooms;

public static class RoomExtensions
{
    public static RoomDto ToRoomDto(this Room room)
    {
        return new RoomDto(
            room.Id,
            room.Number,
            ((int)room.Type));
    }

    public static FloorDto ToFloorDto(this Floor floor)
    {
        return new FloorDto(
            floor.Id,
            floor.Number);
    }
}
