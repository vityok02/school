using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions;

public static class PositionsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/positions", 
            async (IPositionRepository repository, int id) =>
            {
                var positions = await repository.GetAllPositions(id);

                return positions.Select(p => new PositionDto(p.Id, p.Name));
            });

        app.MapGet("/schools/{id}/positions/{positionId}", 
            async (IPositionRepository repository, int positionId, int id) =>
            {
                var position = await repository.GetAsync(positionId);
                

                //if (position is null || position.Schools)
            });

        //app.MapPost("/schools/{id}/positions", null!);

        //app.MapPut("/schools/{id}/positions/{positionId}", null!);

        //app.MapDelete("/schools/{id}/positions/{positionId}", null!);
    }
}
