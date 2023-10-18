using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Floors.Handlers;

public static class DeleteFloorHandler
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

        await repository.DeleteAsync(floor);
        return Results.NoContent();
    }
}
