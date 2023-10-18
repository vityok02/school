using SchoolManagement.API.Filters;
using SchoolManagement.API.Floors.Dtos;
using SchoolManagement.API.Floors.Handlers;

namespace SchoolManagement.API.Floors;

public static class FloorsEndpoints
{
    public static void Map(WebApplication app)
    {
        var floorsGroup = app.MapGroup("/schools/{schoolId:int}/floors")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Floors group")
            .WithOpenApi();

        floorsGroup.MapGet("/", GetAllFloorsHandler.Handle)
            .WithSummary("Get floors")
            .Produces<FloorDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        floorsGroup.MapGet("/{floorId:int}", GetFloorByIdHandler.Handle)
            .WithSummary("Get floor by id")
            .Produces<FloorDto>()
            .Produces(StatusCodes.Status404NotFound);

        floorsGroup.MapPost("/", CreateFloorHandler.Handle)
            .WithSummary("Create floor")
            .Produces<FloorDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        floorsGroup.MapPut("/{floorId:int}", UpdateFloorHandler.Handle)
            .WithSummary("Update floor")
            .Produces<FloorDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        floorsGroup.MapDelete("/{floorId:int}", DeleteFloorHandler.Handle)
            .WithSummary("Delete floor")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
