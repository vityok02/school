using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Floors;
using SchoolManagement.API.Features.Floors.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolManagement.API.Features.Floors.Handlers;

public static class CreateFloorHandler
{
    public static async Task<IResult> Handle(
        IFloorRepository repository,
        [FromRoute] int schoolId,
        [FromBody] FloorCreateDto floorDto)
    {
        bool isDublicate = await repository
            .AnyAsync(f =>
                f.SchoolId == schoolId &&
                f.Number == floorDto.Number);

        if (isDublicate)
        {
            return Results.Conflict(FloorErrorMessages.Dublicate);
        }

        Floor floor = new()
        {
            Number = floorDto.Number,
            SchoolId = schoolId
        };

        await repository.AddAsync(floor);

        var createdFloorDto = floor.ToFloorDto();
        return Results.Created($"/schools/{schoolId}/floors/{floor.Id}", createdFloorDto);
    }
}
