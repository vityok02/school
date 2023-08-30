using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Rooms.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Rooms.Handlers;

public static class GetAllRoomsHandler
{
    public static async Task<IResult> Handle(IRoomRepository repository, [FromRoute] int schoolId)
    {
        var rooms = await repository.GetRoomsAsync(schoolId);

        var roomItemsDto = rooms.Select(r => r.ToRoomDto());
        return Results.Ok(roomItemsDto);
    }
}
