using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Schools;
using SchoolManagement.API.Features.Schools.Dtos;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Schools.Handlers;

public static class CreateSchoolHandler
{
    public static async Task<IResult> Handle(
        HttpContext context,
        IValidator<ISchoolDto> validator,
        ISchoolService schoolService,
        [FromBody] SchoolCreateDto schoolDto)
    {
        var validationResult = await validator.ValidateAsync(schoolDto);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        CreatedLink<SchoolDto> link = await schoolService.CreateSchool(context, schoolDto);

        return Results.Created(link.Uri, link.Value);
    }
}
