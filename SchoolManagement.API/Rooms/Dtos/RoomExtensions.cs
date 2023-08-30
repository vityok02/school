using SchoolManagement.Models;

namespace SchoolManagement.API.Rooms.Dtos;

public static class RoomExtensions
{
    public static RoomDto ToRoomDto(this Room room)
    {
        return new RoomDto(
            room.Id,
            room.Number,
            room.Type.ToString(),
            room.Floor.Number);
    }
}
