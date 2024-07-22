using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Floors;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Floors.Handlers;

public static class DeleteFloorHandler
{
    public static async Task<IResult> Handle(
        IFloorRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int floorId)
    {
        var floor = await repository.GetSchoolFloorAsync(schoolId, floorId);

        if (floor is null || floor.SchoolId != schoolId)
        {
            return Results.NotFound(FloorErrorMessages.NotFound);
        }

        await repository.DeleteAsync(floor);
        return Results.NoContent();
    }
}
