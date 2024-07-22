using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Positions.Handlers.Positions;

public class UpdatePositionHandler
{
    public static async Task<IResult> Handle(
        IValidator<IPositionDto> validator,
        IPositionRepository repository,
        [FromRoute] int positionId,
        [FromBody] PositionDto positionDto)
    {
        var validationResult = await validator.ValidateAsync(positionDto);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (await repository.AnyAsync(p => p.Name == positionDto.Name && p.Id != positionDto.Id))
        {
            return Results.Conflict(PositionErrorMessages.Dublicate);
        }

        var position = await repository.GetAsync(positionId);

        if (position is null)
        {
            return Results.NotFound(PositionErrorMessages.NotFound);
        }

        position.Name = positionDto.Name;

        await repository.UpdateAsync(position);

        var updatedPositionDto = position.ToPositionDto();
        return Results.Ok(updatedPositionDto);
    }
}
