using SchoolManagement.Models;

namespace SchoolManagement.API.Floors.Dtos;

public static class FloorExtensions
{
    public static FloorDto ToFloorDto(this Floor floor)
    {
        return new FloorDto(
            floor.Id,
            floor.Number,
            floor.Rooms.Select(r => r.ToRoomDto()).ToArray());
    }

    public static FloorRoomDto ToRoomDto(this Room room)
    {
        return new FloorRoomDto(
            room.Number,
            room.Type.ToString());
    }
}
