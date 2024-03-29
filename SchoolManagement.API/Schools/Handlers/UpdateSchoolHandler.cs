﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Schools.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Schools.Handlers;

public static class UpdateSchoolHandler
{
    public static async Task<IResult> Handle(
        IValidator<ISchoolDto> validator,
        ISchoolRepository repository,
        [FromRoute] int schoolId,
        [FromBody] SchoolUpdateDto schoolDto)
    {
        var validationResult = await validator.ValidateAsync(schoolDto);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var school = await repository.GetSchoolAsync(schoolId);

        if (school is null) 
        {
            return Results.NotFound("No such school found");
        }

        school.Name = schoolDto.Name;
        school.Address.Country = schoolDto.Country;
        school.Address.City = schoolDto.City;
        school.Address.Street = schoolDto.Street;
        school.Address.PostalCode = schoolDto.PostalCode;
        school.OpeningDate = schoolDto.OpeningDate;

        await repository.UpdateAsync(school);

        var updatedSchoolDto = school.ToSchoolDetailsDto();
        return Results.Ok(updatedSchoolDto);
    }
}
