using SchoolManagement.API.Schools.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Schools.Handlers;

public static class GetAllSchoolsHandler
{
    public static async Task<IResult> Handle(ISchoolRepository repository)
    {
        var schools = await repository.GetSchools();

        var schoolItemDtos = schools.Select(s => s.ToSchoolItemDto());
        return Results.Ok(schoolItemDtos);
    }
}
