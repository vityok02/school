using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Endpoints;

public static class PositionsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/positions", async (IPositionRepository repository, int id) =>
            await repository.GetAllPositions(id));

        app.MapGet("/schools/{id}/positions/{positionId}", async (IPositionRepository repository, int positionId) =>
            await repository.GetPosition(positionId));

        //app.MapPost("/schools/{id}/positions", null!);

        //app.MapPut("/schools/{id}/positions/{positionId}", null!);

        //app.MapDelete("/schools/{id}/positions/{positionId}", null!);
    }
}
