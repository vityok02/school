using SchoolManagement.API.Positions.Handlers;

namespace SchoolManagement.API.Positions;

public static class PositionsEndpoints
{
    public static void Map(WebApplication app)
    {
        var positionsGroup = app.MapGroup("/schools/{schoolId}/positions");

        positionsGroup.MapGet("/", GetAllPositionsHandler.Handle);
        positionsGroup.MapGet("/{positionId}", GetPositionByIdHandler.Handle);
        positionsGroup.MapPost("/", CreatePositionHandler.Handle);
        positionsGroup.MapPut("/{positionId}", UpdatePositionHandler.Handle);
        positionsGroup.MapDelete("/{positionId}", DeletePositionHander.Handle);
    }
}
