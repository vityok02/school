using SchoolManagement.Models.Constants;
using SchoolManagement.API.Filters;
using SchoolManagement.API.Floors.Dtos;
using SchoolManagement.API.Floors.Handlers;

namespace SchoolManagement.API.Floors;

public static class FloorsEndpoints
{
    public static void Map(WebApplication app)
    {
        var manageFloorsGroup = app.MapGroup("/schools/{schoolId:int}/floors")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Manage floors group")
            .WithOpenApi()
            .RequireAuthorization(builder =>
                builder.RequireClaim(ClaimNames.Permissions, Permissions.CanManageFloors));

        var floorsInfoGroup = app.MapGroup("/schools/{schoolId:int}/floors")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Floors info group")
            .WithOpenApi()
            .RequireAuthorization(Policies.CanViewInfo);

        floorsInfoGroup.MapGet("/", GetAllFloorsHandler.Handle)
            .WithSummary("Get floors")
            .Produces<FloorDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        floorsInfoGroup.MapGet("/{floorId:int}", GetFloorByIdHandler.Handle)
            .WithSummary("Get floor by id")
            .Produces<FloorDto>()
            .Produces(StatusCodes.Status404NotFound);

        manageFloorsGroup.MapPost("/", CreateFloorHandler.Handle)
            .WithSummary("Create floor")
            .Produces<FloorDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        manageFloorsGroup.MapPut("/{floorId:int}", UpdateFloorHandler.Handle)
            .WithSummary("Update floor")
            .Produces<FloorDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        manageFloorsGroup.MapDelete("/{floorId:int}", DeleteFloorHandler.Handle)
            .WithSummary("Delete floor")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
