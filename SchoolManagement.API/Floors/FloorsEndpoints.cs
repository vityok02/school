using SchoolManagement.API.Floors.Handlers;

namespace SchoolManagement.API.Floors;

public static class FloorsEndpoints
{
    public static void Map(WebApplication app)
    {
        var floorsGroup = app.MapGroup("/schools/{schoolId}/floors");

        floorsGroup.MapGet("/", GetAllFloorsHandler.Handle);
        floorsGroup.MapGet("/{floorId}", GetFloorByIdHandler.Handle);
        floorsGroup.MapPost("/", CreateFloorHandler.Handle);
        floorsGroup.MapPut("/{floorId}", UpdateFloorHandler.Handle);
        floorsGroup.MapDelete("/{floorId}", DeleteFloorHandler.Handle);
    }
}
