using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Endpoints;

public static class RoomEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/rooms", async (IRoomRepository repository, int id, int floorId) =>
            await repository.GetRoomsAsync(id));

        app.MapPost("/schools/{id}/rooms", null!);

        app.MapGet("/schools/{id}/rooms/{roomId}", null!);

        app.MapPut("/schools/{id}/rooms{roomId}", null!);

        app.MapDelete("/schools{id}/rooms{roomId}", null!);
    }
}