using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Positions.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Positions.Handlers.Positions
{
    public class CreatePositionHandler
    {
        public static async Task<IResult> Handle(
            HttpContext context,
            IValidator<IPositionDto> validator,
            IPositionRepository positionRepository,
            LinkGenerator linkGenerator,
            [FromBody] PositionCreateDto positionDto)
        {
            var validationResult = await validator.ValidateAsync(positionDto);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            if (await positionRepository.AnyAsync(p => p.Name == positionDto.Name))
            {
                return Results.Conflict(PositionErrorMessages.Dublicate);
            }

            var position = new Position()
            {
                Name = positionDto.Name,
            };

            await positionRepository.AddAsync(position);

            var createdPositionDto = position.ToPositionDto();

            var createdLink = linkGenerator.GetUriByName(context, "PositionDetails", new
            {
                positionId = createdPositionDto.Id
            });

            return Results.Created(createdLink!, createdPositionDto);
        }
    }
}
