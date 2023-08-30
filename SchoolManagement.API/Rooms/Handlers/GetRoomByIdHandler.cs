using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Rooms.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Rooms.Handlers;

public static class GetRoomByIdHandler
{
    public static async Task<IResult> Handle(
        IRoomRepository repository, 
        [FromRoute] int schoolId, 
        [FromRoute] int roomId)
    {
        var room = await repository.GetRoomAsync(roomId);

        if (room is null || room.Floor.SchoolId != schoolId)
        {
            return Results.NotFound("No such room found");
        }

        var roomDto = room.ToRoomDto();
        return Results.Ok(roomDto);
    }
}
