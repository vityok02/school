using SchoolManagement.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Floors.Dtos;

namespace SchoolManagement.API.Features.Floors.Handlers;

public static class GetAllFloorsHandler
{
    public static async Task<IResult> Handle(
        IFloorRepository repository, 
        [FromRoute] int schoolId,
        string? sortOrder,
        int page,
        int pageSize)
    {
        var floorsQuery = repository
            .GetFloorsQuery(schoolId, sortOrder)
            .Select(f => f.ToFloorDto());

        var floors = await PagedList<FloorDto>.CreateAsync(floorsQuery, page, pageSize);

        return Results.Ok(floors);
    }
}
