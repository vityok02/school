using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Endpoints;

public static class RoomsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/rooms", async (IRoomRepository repository, int id) =>
            await repository.GetRoomsAsync(id));

        app.MapGet("/schools/{id}/rooms/{roomId}", async (IRoomRepository repository, int id, int roomId) =>
            await repository.GetAsync(roomId));

        //app.MapPost("/schools/{id}/rooms", null!);

        //app.MapPut("/schools/{id}/rooms/{roomId}", null!);

        //app.MapDelete("/schools/{id}/rooms/{roomId}", null!);
    }
}