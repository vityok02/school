using SchoolManagement.Models.Constants;
using SchoolManagement.API.Filters;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.API.Positions.Handlers.SchoolPositions;

namespace SchoolManagement.API.Positions;

public static class SchoolPositionsEndpoints
{
    public static void Map(WebApplication app)
    {
        var managePositionsGroup = app.MapGroup("/schools/{schoolId:int}/positions")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Manage schools positions group")
            .WithOpenApi()
            .RequireAuthorization(builder =>
                builder.RequireClaim(ClaimNames.Permissions, Permissions.CanManageSchoolPositions));

        var positionsInfoGroup = app.MapGroup("/schools/{schoolId:int}/positions")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Schools positions info group")
            .WithOpenApi()
            .RequireAuthorization(Policies.CanViewInfo);

        positionsInfoGroup.MapGet("/", GetAllSchoolPositionsHandler.Handle)
            .WithSummary("Get positions by school id")
            .WithName("SchoolPositions")
            .Produces<PositionDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        positionsInfoGroup.MapGet("/{positionId:int}", GetSchoolPositionByIdHandler.Handle)
            .WithSummary("Get school position by id")
            .WithName("SchoolPosition")
            .Produces<PositionDto>()
            .Produces(StatusCodes.Status404NotFound);

        managePositionsGroup.MapPost("/", AddPositionToSchoolHandler.Handle)
            .WithSummary("Add position to school")
            .Produces<PositionDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        managePositionsGroup.MapDelete("/{positionId:int}", DeletePositionFromSchoolHandler.Handle)
            .WithSummary("Delete position from school")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
