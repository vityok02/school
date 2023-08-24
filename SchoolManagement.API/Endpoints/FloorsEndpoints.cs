using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Endpoints;

public static class FloorsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/floors", async (IFloorRepository repository, int id) =>
            await repository.GetAllAsync(f => f.SchoolId == id));

        app.MapGet("/schools/{id}/floors/{floorId}", async (IFloorRepository repository, int floorId) =>
            await repository.GetAsync(floorId));

        app.MapPost("/schools/{id}/floors", async (IFloorRepository repository, int number, int id) =>
        {
            await Console.Out.WriteLineAsync("Success");

            Floor floor = new()
            {
                Number = number,
                SchoolId = id
            };

            await repository.AddAsync(floor);
        });

        app.MapPut("/schools/{id}/floors/{floorId}", async (IFloorRepository repository, int floorId, int number) =>
        {
            var floor = await repository.GetAsync(floorId);
            floor!.Number = number;

            await repository.UpdateAsync(floor);
        });

        app.MapDelete("/schools/{id}/floors/{floorId}", async (IFloorRepository repository, int floorId) =>
        {
            var floor = await repository.GetFloorAsync(floorId);
            await repository.DeleteAsync(floor);
        });
    }
}
