using SchoolManagement.API.Filters;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.API.Positions.Handlers.SchoolPositions;

namespace SchoolManagement.API.Positions;

public static class SchoolPositionsEndpoints
{
    public static void Map(WebApplication app)
    {
        var positionsGroup = app.MapGroup("/schools/{schoolId:int}/positions")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Schools positions group")
            .WithOpenApi();

        positionsGroup.MapGet("/", GetAllSchoolPositionsHandler.Handle)
            .WithSummary("Get positions by school")
            .WithName("SchoolPositions")
            .Produces<PositionDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        positionsGroup.MapGet("/{positionId:int}", GetSchoolPositionByIdHandler.Handle)
            .WithSummary("Get school position by id")
            .WithName("SchoolPosition")
            .Produces<PositionDto>()
            .Produces(StatusCodes.Status404NotFound);

        positionsGroup.MapPost("/", AddPositionToSchoolHandler.Handle)
            .WithSummary("Add position to school")
            .Produces<PositionDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        positionsGroup.MapDelete("/{positionId:int}", DeletePositionFromSchoolHandler.Handle)
            .WithSummary("Delete position from school")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
