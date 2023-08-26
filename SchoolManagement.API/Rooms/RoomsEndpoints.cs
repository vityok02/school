using SchoolManagement.API.Rooms.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Rooms;

public static class RoomsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/rooms",
            async (IRoomRepository repository, int id) =>
            {
                var rooms = await repository.GetRoomsAsync(id);
                return rooms.Select(r => r.ToRoomDto());
            });

        app.MapGet("/schools/{id}/rooms/{roomId}",
            async (IRoomRepository repository, int id, int roomId) =>
            {
                var room = await repository.GetRoomAsync(roomId);

                if (room is null || room.Floor.SchoolId != id)
                {
                    return Results.NotFound("No such room found");
                }

                var roomDto = room.ToRoomDto();
                return Results.Json(roomDto);
            });

        app.MapPost("/schools/{id}/rooms",
            async (IRoomRepository repository, int id, RoomAddDto roomDto) =>
            {
                var room = new Room()
                {
                    Number = roomDto.Number,
                    Type = roomDto.Types,
                    FloorId = roomDto.FloorId,
                };

                await repository.AddAsync(room);
            });

        app.MapPut("/schools/{id}/rooms/{roomId}",
            async (IRoomRepository repository, int id, RoomEditDto roomDto, int roomId) =>
            {
                var room = await repository.GetRoomAsync(roomId);

                if (room is null || room.Floor.SchoolId != id)
                {
                    return Results.NotFound("No such room found");
                }

                room.Number = roomDto.Number;
                room.Type = roomDto.Types;
                room.FloorId = roomDto.FloorId;

                await repository.UpdateAsync(room);
                return Results.Json(room);
            });

        app.MapDelete("/schools/{id}/rooms/{roomId}",
            async (IRoomRepository repository, int roomId, int id) =>
            {
                var room = await repository.GetRoomAsync(roomId);

                if(room is null || room.Floor.SchoolId != id)
                {
                    return Results.NotFound("No such room found");
                }

                await repository.DeleteAsync(room);
                return Results.Ok();
            });
    }
}