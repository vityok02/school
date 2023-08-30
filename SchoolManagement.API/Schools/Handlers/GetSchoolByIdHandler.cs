using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Schools.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Schools.Handlers;

public static class GetSchoolByIdHandler
{
    public static async Task<IResult> Handle(ISchoolRepository repository, [FromRoute] int schoolId)
    {
        var school = await repository.GetSchoolAsync(schoolId);

        if (school is null)
        {
            return Results.NotFound("No such school found");
        }

        var schoolDto = school.ToSchoolInfoDto();
        return Results.Ok(schoolDto);
    }
}
