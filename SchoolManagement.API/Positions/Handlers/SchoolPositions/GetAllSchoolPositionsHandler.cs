using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers.SchoolPositions;

public static class GetAllSchoolPositionsHandler
{
    public static async Task<IResult> Handle(IPositionRepository repository,
        [FromRoute] int schoolId)
    {
        var positions = await repository.GetAllPositions(schoolId);

        var positionItemDtos = positions.Select(p => p.ToPositionDto());
        return Results.Ok(positionItemDtos);
    }
}
