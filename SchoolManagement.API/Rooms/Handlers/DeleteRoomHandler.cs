using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Rooms.Handlers;

public static class DeleteRoomHandler
{
    public static async Task<IResult> Handle(
        IRoomRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int roomId)
    {
        var room = await repository.GetRoomAsync(roomId);

        if (room is null || room.Floor.SchoolId != schoolId)
        {
            return Results.NotFound(RoomErrorMessages.RoomNotFound);
        }

        await repository.DeleteAsync(room);
        return Results.NoContent();
    }
}