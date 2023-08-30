using SchoolManagement.API.Students.Handlers;

namespace SchoolManagement.API.Students;

public static class StudentsEndpoints
{
    public static void Map(WebApplication app)
    {
        var studentsGroup = app.MapGroup("/schools/{schoolId}/students");

        studentsGroup.MapGet("/", GetAllStudentsHandler.Handle);
        studentsGroup.MapGet("/{studentId}", GetStudentByIdHandler.Handle);
        studentsGroup.MapPost("/", CreateStudentHandler.Handle);
        studentsGroup.MapPut("/{studentId}", UpdateStudentHandler.Handle);
        studentsGroup.MapDelete("/{studentId}", DeleteStudentHandler.Handle);
    }
}
