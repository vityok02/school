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
        var floor = await repository.GetSchoolFloorAsync(schoolId, floorId);

        if (floor is null)
        {
            return Results.NotFound(FloorErrorMessages.NotFound);
        }

        var floorDto = floor.ToFloorDto();
        return Results.Ok(floorDto);
    }
}
