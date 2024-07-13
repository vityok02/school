using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Positions;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Positions.Handlers.Positions;

public static class DeletePositionHander
{
    public static async Task<IResult> Handle(IPositionRepository repository,
        [FromRoute] int positionId)
    {
        var position = await repository.GetAsync(positionId);

        if (position is null)
        {
            return Results.NotFound(PositionErrorMessages.NotFound);
        }

        await repository.DeleteAsync(position);
        return Results.NoContent();
    }
}
