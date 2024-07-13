using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Schools;
using SchoolManagement.API.Schools.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Schools.Handlers;

public static class GetSchoolByIdHandler
{
    public static async Task<IResult> Handle(ISchoolService schoolService, [FromRoute] int schoolId)
    {
        var school = schoolService.GetSchoolById(schoolId);

        return Results.Ok(school);
    }
}
