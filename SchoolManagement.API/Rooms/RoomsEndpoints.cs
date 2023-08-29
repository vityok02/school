using SchoolManagement.API.Rooms.Handlers;

namespace SchoolManagement.API.Rooms;

public static class RoomsEndpoints
{
    public static void Map(WebApplication app)
    {
        var roomsGroup = app.MapGroup("/schools/{schoolId}/rooms");

        roomsGroup.MapGet("/", GetAllRoomsHandler.Handle);
        roomsGroup.MapGet("/{roomId}", GetRoomByIdHandler.Handle);
        roomsGroup.MapPost("/", CreateRoomHandler.Handle);
        roomsGroup.MapPut("/{roomId}", UpdateRoomHandler.Handle);
        roomsGroup.MapDelete("/{roomId}", UpdateRoomHandler.Handle);
    }
}