using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Schools.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Schools.Handlers;

public static class CreateSchoolHandler
{
    public static async Task<IResult> Handle(
        IValidator<ISchoolDto> validator,
        ISchoolRepository repository,
        [FromBody] SchoolCreateDto schoolDto)
    {
        var validationResult = await validator.ValidateAsync(schoolDto);

        if (!validationResult.IsValid) 
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var address = new Address()
        {
            Country = schoolDto.Country,
            City = schoolDto.City,
            Street = schoolDto.Street,
            PostalCode = schoolDto.PostalCode,
        };

        var school = new School()
        {
            Name = schoolDto.Name,
            Address = address,
            OpeningDate = schoolDto.OpeningDate,
        };

        await repository.AddAsync(school);

        var createdSchoolDto = school.ToSchoolDetailsDto();
        return Results.Created($"/schools/{school.Id}", createdSchoolDto);
    }
}
