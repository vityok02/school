using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers.Positions;

public static class DeletePositionHander
{
    public static async Task<IResult> Handle(IPositionRepository repository,
        [FromRoute] int positionId)
    {
        var position = await repository.GetAsync(positionId);

        if (position is null)
        {
            return Results.NotFound("No such positionFound");
        }

        await repository.DeleteAsync(position);
        return Results.NoContent();
    }
}
