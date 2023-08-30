using SchoolManagement.API.Schools.Handlers;

namespace SchoolManagement.API.Schools;

public static class SchoolsEndpoints
{
    public static void Map(WebApplication app)
    {
        var schoolsGroup = app.MapGroup("/schools");

        schoolsGroup.MapGet("/", GetAllSchoolsHandler.Handle);
        schoolsGroup.MapGet("/{schoolId}", GetSchoolByIdHandler.Handle);
        schoolsGroup.MapPost("/", CreateSchoolHandler.Handle);
        schoolsGroup.MapPut("/{schoolId}", UpdateSchoolHandler.Handle);
        schoolsGroup.MapDelete("/{schoolId}", DeleteSchoolHandler.Handle);
    }
}
