using SchoolManagement.Models.Constants;
using SchoolManagement.API.Filters;
using SchoolManagement.API.Rooms.Dtos;
using SchoolManagement.API.Rooms.Handlers;

namespace SchoolManagement.API.Rooms;

public static class RoomsEndpoints
{
    public static void Map(WebApplication app)
    {
        var manageRoomsGroup = app.MapGroup("/schools/{schoolId:int}/rooms")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Manage rooms group")
            .WithOpenApi()
            .RequireAuthorization(builder => 
                builder.RequireClaim(ClaimNames.Permissions, Permissions.CanManageRooms));
        
        var roomsInfoGroup = app.MapGroup("/schools/{schoolId:int}/rooms")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Rooms info group")
            .WithOpenApi()
            .RequireAuthorization(Policies.CanViewInfo);

        roomsInfoGroup.MapGet("/", GetAllRoomsHandler.Handle)
            .WithSummary("Get all rooms")
            .Produces<RoomDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        roomsInfoGroup.MapGet("/{roomId:int}", GetRoomByIdHandler.Handle)
            .WithSummary("Get room by id")
            .Produces<RoomDto>()
            .Produces(StatusCodes.Status404NotFound);

        manageRoomsGroup.MapPost("/", CreateRoomHandler.Handle)
            .WithSummary("Create room")
            .Produces<RoomDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        manageRoomsGroup.MapPut("/{roomId:int}", UpdateRoomHandler.Handle)
            .WithSummary("Update room")
            .Produces<RoomDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        manageRoomsGroup.MapDelete("/{roomId:int}", DeleteRoomHandler.Handle)
            .WithSummary("Delete room")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}