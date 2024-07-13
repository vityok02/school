using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Schools;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Schools.Handlers;

public static class DeleteSchoolHandler
{
    public static async Task<IResult> Handle(
        ISchoolRepository repository,
        [FromRoute] int schoolId)
    {
        var school = await repository.GetAsync(schoolId);

        if (school is null)
        {
            return Results.NotFound(SchoolErrors.NotFound);
        }

        await repository.DeleteAsync(school);
        return Results.NoContent();
    }
}
