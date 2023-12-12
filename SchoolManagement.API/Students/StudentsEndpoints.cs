using SchoolManagement.Models.Constants;
using SchoolManagement.API.Filters;
using SchoolManagement.API.Students.Dtos;
using SchoolManagement.API.Students.Handlers;

namespace SchoolManagement.API.Students;

public static class StudentsEndpoints
{
    public static void Map(WebApplication app)
    {
        var manageStudentsGroup = app.MapGroup("/schools/{schoolId:int}/students")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Manage students group")
            .WithOpenApi()
            .RequireAuthorization(builder =>
                builder.RequireClaim(ClaimNames.Permissions, Permissions.CanManageStudents));

        var studentsInfoGroup = app.MapGroup("/schools/{schoolId:int}/students")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Students info group")
            .WithOpenApi()
            .RequireAuthorization(Policies.CanViewInfo);

        studentsInfoGroup.MapGet("/", GetAllStudentsHandler.Handle)
            .WithSummary("Get students")
            .Produces<StudentDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        studentsInfoGroup.MapGet("/{studentId:int}", GetStudentByIdHandler.Handle)
            .WithSummary("Get student by id")
            .Produces<StudentDto>()
            .Produces(StatusCodes.Status404NotFound);

        manageStudentsGroup.MapPost("/", CreateStudentHandler.Handle)
            .WithSummary("Create student")
            .Produces<StudentDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        manageStudentsGroup.MapPut("/{studentId:int}", UpdateStudentHandler.Handle)
            .WithSummary("Create student")
            .Produces<StudentDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        manageStudentsGroup.MapDelete("/{studentId:int}", DeleteStudentHandler.Handle)
            .WithSummary("Delete student")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
