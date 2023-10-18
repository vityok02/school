using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers.Positions;

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

        var positions = await repository.GetAllAsync();

        if (positions.Any(p => p.Name == positionDto.Name && p.Id != positionDto.Id))
        {
            return Results.BadRequest("There is already a position with that name");
        }

        var position = await repository.GetAsync(positionId);

        if (position is null)
        {
            return Results.NotFound("No such position found");
        }

        position.Name = positionDto.Name;

        await repository.UpdateAsync(position);

        var updatedPositionDto = position.ToPositionDto();
        return Results.Ok(updatedPositionDto);
    }
}
