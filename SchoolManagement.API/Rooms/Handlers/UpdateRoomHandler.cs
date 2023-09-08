using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Rooms.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Rooms.Handlers;

public static class UpdateRoomHandler
{
    public static async Task<IResult> Handle(
        IRoomRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int roomId,
        [FromBody] RoomUpdateDto roomDto)
    {
        var rooms = await repository.GetRoomsForSchoolAsync(schoolId);

        if (rooms.Any(r => r.Number == roomDto.Number))
        {
            return Results.BadRequest("Room with this number already exists");
        }

        var room = await repository.GetRoomAsync(roomId);

        if (room is null || room.Floor.SchoolId != schoolId)
        {
            return Results.NotFound("No such room found");
        }

        room.Number = roomDto.Number;
        room.Type = roomDto.Types;
        room.FloorId = roomDto.FloorId;

        await repository.UpdateAsync(room);

        var updatedRoomDto = room.ToRoomDto();
        return Results.Ok(updatedRoomDto);
    }
}
