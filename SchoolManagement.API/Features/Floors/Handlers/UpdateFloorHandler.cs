using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Floors;
using SchoolManagement.API.Features.Floors.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Floors.Handlers;

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
            return Results.NotFound(FloorErrorMessages.NotFound);
        }

        bool isDublicate = await repository
            .AnyAsync(f => f.SchoolId == schoolId
                && f.Number != floor.Number
                && f.Number == floorDto.Number);

        if (isDublicate)
        {
            return Results.Conflict(FloorErrorMessages.Dublicate);
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
