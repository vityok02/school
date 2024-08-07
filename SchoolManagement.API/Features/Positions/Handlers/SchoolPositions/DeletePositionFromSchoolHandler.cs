﻿using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Positions;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Positions.Handlers.SchoolPositions;

public class DeletePositionFromSchoolHandler
{
    public static async Task<IResult> Handle(
        ISchoolRepository schoolRepository,
        IPositionRepository positionRepository,
        [FromRoute] int schoolId,
        [FromRoute] int positionId)
    {
        var school = await schoolRepository.GetByIdAsync(schoolId);

        var position = await positionRepository.GetByIdAsync(positionId);
        if (position is null)
        {
            return Results.NotFound(PositionErrorMessages.NotFound);
        }

        school!.Positions.Remove(position);

        return Results.NoContent();
    }
}