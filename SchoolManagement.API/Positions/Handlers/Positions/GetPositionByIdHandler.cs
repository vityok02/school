﻿using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers.Positions;

public static class GetPositionByIdHandler
{
    public static async Task<IResult> Handle(
        IPositionRepository repository,
        [FromRoute] int positionId)
    {
        var position = await repository.GetAsync(positionId);

        if (position is null)
        {
            return Results.NotFound(PositionErrorMessages.NotFound);
        }

        var positionDto = position.ToPositionDto();
        return Results.Ok(positionDto);
    }
}
