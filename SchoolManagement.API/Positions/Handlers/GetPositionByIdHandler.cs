using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers;

public static class GetPositionByIdHandler
{
    public static async Task<IResult> Handle(
        IPositionRepository repository, 
        [FromRoute] int schoolId, 
        [FromRoute] int positionId)
    {
        var position = await repository.GetPositionForSchool(positionId, schoolId);

        if (position is null)
        {
            return Results.NotFound("No such position found");
        }

        var positionDto = position.ToPositionDto();
        return Results.Ok(positionDto);
    }
}
