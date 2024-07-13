using SchoolManagement.Models.Interfaces;
using SchoolManagement.API.Floors.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.API.Floors.Handlers;

public static class GetAllFloorsHandler
{
    public static async Task<IResult> Handle(IFloorRepository repository, [FromRoute] int schoolId)
    {
        var floors = await repository.GetFloorsAsync(schoolId);

        var floorItemDtos = floors.Select(f => f.ToFloorDto());
        return Results.Ok(floorItemDtos);
    }
}
