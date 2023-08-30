using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Schools.Handlers;

public static class DeleteSchoolHandler
{
    public static async Task<IResult> Handle(
        ISchoolRepository repository,
        [FromRoute] int schoolId)
    {
        var school = await repository.GetAsync(schoolId);

        if (school is null)
        {
            return Results.NotFound("No such school found");
        }

        await repository.DeleteAsync(school);
        return Results.Ok();
    }
}
