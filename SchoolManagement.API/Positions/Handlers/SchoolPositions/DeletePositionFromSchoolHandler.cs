using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers.SchoolPositions;

public class DeletePositionFromSchoolHandler
{
    public static async Task<IResult> Handle(
        ISchoolRepository schoolRepository,
        IPositionRepository positionRepository,
        [FromRoute] int schoolId,
        [FromRoute] int positionId)
    {
        var school = await schoolRepository.GetAsync(schoolId);

        var position = await positionRepository.GetAsync(positionId);
        if (position is null)
        {
            return Results.NotFound(PositionErrorMessages.NotFound);
        }

        school!.Positions.Remove(position);

        return Results.NoContent();
    }
}