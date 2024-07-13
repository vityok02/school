using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers.SchoolPositions;

public static class AddPositionToSchoolHandler
{
    public static async Task<IResult> Handle(
        IPositionRepository positionRepository,
        ISchoolRepository schoolRepository,
        LinkGenerator linkGenerator,
        [FromRoute] int schoolId,
        [FromBody] PositionIdDto positionIdDto)
    {
        var position = await positionRepository.GetPosition(positionIdDto.Id);

        if (position is null)
        {
            return Results.NotFound(PositionErrorMessages.NotFound);
        }

        var school = await schoolRepository.GetSchoolAsync(schoolId);

        if (school.Positions.Any(p => p.Id == position.Id))
        {
            return Results.Conflict(PositionErrorMessages.DublicateInSchool);
        }

        school.Positions.Add(position);

        await schoolRepository.SaveChangesAsync();

        var schoolPositionsLink = linkGenerator.GetPathByName("SchoolPositions", new
        {
            schoolId
        });

        return Results.Created(schoolPositionsLink!, position.ToPositionDto());
    }
}
