using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Rooms.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Rooms.Handlers;

public static class GetAllRoomsHandler
{
    public static async Task<IResult> Handle(
        IRoomRepository repository, 
        [FromRoute] int schoolId,
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize)
    {
        var roomsQuery = repository
            .GetRoomsQuery(schoolId, searchTerm, sortColumn, sortOrder)
            .Select(r => r.ToRoomDto());

        var rooms = await PagedList<RoomDto>.CreateAsync(roomsQuery, page, pageSize);
        
        return Results.Ok(rooms);
    }
}
