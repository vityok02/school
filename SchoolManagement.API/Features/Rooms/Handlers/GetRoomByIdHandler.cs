﻿using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Rooms.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Rooms.Handlers;

public static class GetRoomByIdHandler
{
    public static async Task<IResult> Handle(
        IRoomRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int roomId)
    {
        var room = await repository.GetSchoolRoomAsync(schoolId, roomId);

        if (room is null)
        {
            return Results.NotFound(RoomErrorMessages.RoomNotFound);
        }

        var roomDto = room.ToRoomDto();
        return Results.Ok(roomDto);
    }
}
