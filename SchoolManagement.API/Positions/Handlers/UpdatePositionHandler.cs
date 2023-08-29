using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers;

public class UpdatePositionHandler
{
    public static async Task<IResult> Handle(
        IPositionRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int positionId,
        [FromBody] PositionDto positionDto)
    {
        var position = await repository.GetPositionForSchool(positionId, schoolId);

        if (position is null)
        {
            return Results.NotFound("No such position found");
        }

        position.Name = positionDto.Name;

        await repository.UpdateAsync(position);
        return Results.Ok(position);
    }
}
