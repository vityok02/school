using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Floors.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Floors.Handlers;

public static class UpdateFloorHandler
{
    public static async Task<IResult> Handle(
        IFloorRepository repository, 
        [FromRoute] int schoolId, 
        [FromRoute] int floorId,
        [FromBody] FloorUpdateDto floorDto)
    {
        var floor = await repository.GetAsync(floorId);

        if (floor is null)
        {
            return Results.NotFound("No such floor found");
        }

        var floors = await repository
            .GetAllAsync(f => f.SchoolId == schoolId && f.Number != floor.Number);

        if (floors.Any(f => f.Number == floorDto.Number))
        {
            return Results.BadRequest("A floor with this number already exists");
        }

        if (floorDto.Number != 0)
        {
            floor.Number = floorDto.Number;
        }

        await repository.UpdateAsync(floor);

        var updatedFloorDto = floor.ToFloorDto();
        return Results.Ok(updatedFloorDto);
    }
}
