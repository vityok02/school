using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers;

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
            return Results.NotFound("No such position found");
        }

        var positionDto = position.ToPositionDto();
        return Results.Ok(positionDto);
    }
}