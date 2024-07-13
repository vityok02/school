using SchoolManagement.API.Features.Positions.Dtos;
using SchoolManagement.API.Features.Positions.Handlers.Positions;

namespace SchoolManagement.API.Features.Positions;

public static class PositionsEndpoints
{
    public static void Map(WebApplication app)
    {
        var positionsGroup = app.MapGroup("/positions")
            .WithTags("Positions group")
            .WithOpenApi();

        positionsGroup.MapGet("/", GetAllPositionsHandler.Handle)
            .WithSummary("Get positions")
            .Produces<PositionDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        positionsGroup.MapGet("/{positionId:int}", GetPositionByIdHandler.Handle)
            .WithSummary("Get position by id")
            .WithName("PositionDetails")
            .Produces<PositionDto>()
            .Produces(StatusCodes.Status404NotFound);

        positionsGroup.MapPost("/", CreatePositionHandler.Handle)
            .WithSummary("Create position")
            .Produces<PositionDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status404NotFound);

        positionsGroup.MapPut("/{positionId:int}", UpdatePositionHandler.Handle)
            .WithSummary("Update position")
            .Produces<PositionDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status404NotFound);

        positionsGroup.MapDelete("/{positionId:int}", DeletePositionHander.Handle)
            .WithSummary("Delete position")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
