using SchoolManagement.API.Features.Rooms.Dtos;
using SchoolManagement.API.Features.Rooms.Handlers;
using SchoolManagement.API.Filters;

namespace SchoolManagement.API.Features.Rooms;

public static class RoomsEndpoints
{
    public static void Map(WebApplication app)
    {
        var roomsGroup = app.MapGroup("/schools/{schoolId:int}/rooms")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Rooms group")
            .WithOpenApi();

        roomsGroup.MapGet("/", GetAllRoomsHandler.Handle)
            .WithSummary("Get all rooms")
            .Produces<PagedList<RoomDto>>()
            .Produces(StatusCodes.Status404NotFound);

        roomsGroup.MapGet("/{roomId:int}", GetRoomByIdHandler.Handle)
            .WithSummary("Get room by id")
            .Produces<RoomDto>()
            .Produces(StatusCodes.Status404NotFound);

        roomsGroup.MapPost("/", CreateRoomHandler.Handle)
            .WithSummary("Create room")
            .Produces<RoomDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status404NotFound);

        roomsGroup.MapPut("/{roomId:int}", UpdateRoomHandler.Handle)
            .WithSummary("Update room")
            .Produces<RoomDto>()
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status404NotFound);

        roomsGroup.MapDelete("/{roomId:int}", DeleteRoomHandler.Handle)
            .WithSummary("Delete room")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}