using SchoolManagement.Models.Constants;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.API.Positions.Handlers.Positions;

namespace SchoolManagement.API.Positions;

public static class PositionsEndpoints
{
    public static void Map(WebApplication app)
    {
        var managePositionsGroup = app.MapGroup("/positions")
            .WithTags("Manage positions group")
            .WithOpenApi()
            .RequireAuthorization(builder =>
                builder.RequireClaim(ClaimNames.Permissions, Permissions.CanManagePositions));

        var positionsInfoGroup = app.MapGroup("/positions")
            .WithTags("Positions info group")
            .WithOpenApi()
            .RequireAuthorization(Policies.CanViewInfo);

        positionsInfoGroup.MapGet("/", GetAllPositionsHandler.Handle)
            .WithSummary("Get positions")
            .Produces<PositionDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        positionsInfoGroup.MapGet("/{positionId:int}", GetPositionByIdHandler.Handle)
            .WithSummary("Get position by id")
            .WithName("PositionDetails")
            .Produces<PositionDto>()
            .Produces(StatusCodes.Status404NotFound);

        managePositionsGroup.MapPost("/", CreatePositionHandler.Handle)
            .WithSummary("Create position")
            .Produces<PositionDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        managePositionsGroup.MapPut("/{positionId:int}", UpdatePositionHandler.Handle)
            .WithSummary("Update position")
            .Produces<PositionDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        managePositionsGroup.MapDelete("/{positionId:int}", DeletePositionHander.Handle)
            .WithSummary("Delete position")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
