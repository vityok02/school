using SchoolManagement.API.Floors.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Floors;

public static class FloorsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/floors",
            async (IFloorRepository repository, int id) =>
            {
                var floors = await repository.GetFloorsAsync(id);

                return floors.Select(f => f.ToFloorDto());
            });

        app.MapGet("/schools/{id}/floors/{floorId}",
            async (IFloorRepository repository, int floorId, int id) =>
            {
                var floor = await repository.GetFloorAsync(floorId);

                if (floor is null || floor.SchoolId != id)
                {
                    return Results.NotFound("No such floor found");
                }

                var floorDto = floor.ToFloorDto();
                return Results.Json(floorDto);
            });

        app.MapPost("/schools/{id}/floors",
            async (IFloorRepository repository, int number, int id) =>
            {
                var floors = await repository.GetAllAsync(f => f.SchoolId == id);

                if (floors.Any(f => f.Number == number))
                {
                    return Results.BadRequest("A floor with this number already exists");
                }

                Floor floor = new()
                {
                    Number = number,
                    SchoolId = id
                };

                await repository.AddAsync(floor);

                return Results.Json(floor);
            });

        app.MapPut("/schools/{id}/floors/{floorId}",
            async (IFloorRepository repository, int floorId, int number, int id) =>
            {
                var floor = await repository.GetAsync(floorId);

                if(floor is null)
                {
                    return Results.NotFound("No such floor found");
                }

                var floors = await repository
                    .GetAllAsync(f => f.SchoolId == id && f.Number != floor.Number);

                if(floors.Any(f => f.Number == number))
                {
                    return Results.BadRequest("A floor with this number already exists");
                }

                if (number != 0)
                {
                    floor.Number = number;
                }

                await repository.UpdateAsync(floor);
                return Results.Json(floor);
            });

        app.MapDelete("/schools/{id}/floors/{floorId}",
            async (IFloorRepository repository, int floorId, int id) =>
            {
                var floor = await repository.GetFloorAsync(floorId);

                if (floor is null || floor.SchoolId != id)
                {
                    return Results.NotFound("No such floor found");
                }

                await repository.DeleteAsync(floor);
                return Results.Ok();
            });
    }
}
