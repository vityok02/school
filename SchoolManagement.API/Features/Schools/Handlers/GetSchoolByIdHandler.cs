using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Schools.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Schools.Handlers;

public static class GetSchoolByIdHandler
{
    public static async Task<IResult> Handle(ISchoolRepository repository, [FromRoute] int schoolId)
    {
        var school = await repository.GetSchoolAsync(schoolId);

        return Results.Ok(school.ToSchoolDto());
    }
}
