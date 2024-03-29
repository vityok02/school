﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers.Positions
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

            var existingPositions = await positionRepository.GetAllAsync(p => p.Name == positionDto.Name);

            if (existingPositions.Any())
            {
                return Results.BadRequest($"Position '{positionDto.Name}' already exists");
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
