using SchoolManagement.API.Filters;
using SchoolManagement.API.Students.Dtos;
using SchoolManagement.API.Students.Handlers;

namespace SchoolManagement.API.Students;

public static class StudentsEndpoints
{
    public static void Map(WebApplication app)
    {
        var studentsGroup = app.MapGroup("/schools/{schoolId:int}/students")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Students group")
            .WithOpenApi();

        studentsGroup.MapGet("/", GetAllStudentsHandler.Handle)
            .WithSummary("Get students")
            .Produces<StudentDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        studentsGroup.MapGet("/{studentId:int}", GetStudentByIdHandler.Handle)
            .WithSummary("Get student by id")
            .Produces<StudentDto>()
            .Produces(StatusCodes.Status404NotFound);

        studentsGroup.MapPost("/", CreateStudentHandler.Handle)
            .WithSummary("Create student")
            .Produces<StudentDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status404NotFound);

        studentsGroup.MapPut("/{studentId:int}", UpdateStudentHandler.Handle)
            .WithSummary("Create student")
            .Produces<StudentDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status404NotFound);

        studentsGroup.MapDelete("/{studentId:int}", DeleteStudentHandler.Handle)
            .WithSummary("Delete student")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
