﻿using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Floors.Dtos;

namespace SchoolManagement.API.Floors.Handlers;

public static class CreateFloorHandler
{
    public static async Task<IResult> Handle(
        IFloorRepository repository,
        [FromRoute] int schoolId,
        [FromBody] FloorCreateDto floorDto)
    {
        var floors = await repository.GetAllAsync(f => f.SchoolId == schoolId);

        if (floors.Any(f => f.Number == floorDto.Number))
        {
            return Results.BadRequest("A floor with this number already exists");
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
