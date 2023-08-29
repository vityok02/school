using SchoolManagement.Models.Interfaces;
using SchoolManagement.API.Floors.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.API.Floors.Handlers;

public static class GetFloorByIdHandler
{
    public static async Task<IResult> Handle(
        IFloorRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int floorId)
    {
        var floor = await repository.GetFloorAsync(floorId);

        if (floor is null || floor.SchoolId != schoolId)
        {
            return Results.NotFound("No such floor found");
        }

        var floorDto = floor.ToFloorDto();
        return Results.Json(floorDto);
    }
}
