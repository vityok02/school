using SchoolManagement.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Floors.Dtos;

namespace SchoolManagement.API.Features.Floors.Handlers;

public static class GetAllFloorsHandler
{
    public static async Task<IResult> Handle(IFloorRepository repository, [FromRoute] int schoolId)
    {
        var floors = await repository.GetFloorsAsync(schoolId);

        var floorItemDtos = floors.Select(f => f.ToFloorDto());
        return Results.Ok(floorItemDtos);
    }
}
