using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers;

public static class DeletePositionHander
{
    public static async Task<IResult> Handle(
        IPositionRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int positionId)
    {
        var position = await repository.GetPositionForSchool(positionId, schoolId);

        if (position is null) 
        {
            return Results.NotFound("No such positionFound");
        }

        await repository.DeleteAsync(position);
        return Results.Ok();
    }
}
