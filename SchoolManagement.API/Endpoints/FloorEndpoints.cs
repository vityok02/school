using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Endpoints;

public static class FloorEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/floors", async (IFloorRepository repository, int id) =>
            await repository.GetSchoolFloorsAsync(id));

        app.MapPost("/schools/{id}/floors", null!);

        app.MapGet("/schools/{id}/floors{floorId}", null!);

        app.MapPut("/schools/{id}/floors{floorId}", null!);

        app.MapDelete("/schools{id}/floors{floorId}", null!);
    }
}
