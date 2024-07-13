using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Positions;
using SchoolManagement.API.Features.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Positions.Handlers.SchoolPositions;

public class GetSchoolPositionByIdHandler
{
    public static async Task<IResult> Handle(
        IPositionRepository positionRepository,
        [FromRoute] int schoolId,
        [FromRoute] int positionId)
    {
        var position = await positionRepository.GetSchoolPosition(schoolId, positionId);

        if (position is null)
        {
            return Results.NotFound(PositionErrorMessages.NotFound);
        }

        var positionDto = position.ToPositionDto();
        return Results.Ok(positionDto);
    }
}