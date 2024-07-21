using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Rooms.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Rooms.Handlers;

public static class UpdateRoomHandler
{
    public static async Task<IResult> Handle(
        IRoomRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int roomId,
        [FromBody] RoomUpdateDto roomDto)
    {
        if (await repository.AnyAsync(r => r.Number == roomDto.Number))
        {
            return Results.Conflict(RoomErrorMessages.Dublicate);
        }

        var room = await repository.GetRoomAsync(roomId);

        if (room is null || room.Floor.SchoolId != schoolId)
        {
            return Results.NotFound(RoomErrorMessages.RoomNotFound);
        }

        room.Number = roomDto.Number;
        room.Type = roomDto.Types;
        room.FloorId = roomDto.FloorId;

        await repository.UpdateAsync(room);

        var updatedRoomDto = room.ToRoomDto();
        return Results.Ok(updatedRoomDto);
    }
}
