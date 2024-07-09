using SchoolManagement.API.Filters;
using SchoolManagement.API.Schools.Dtos;
using SchoolManagement.API.Schools.Handlers;

namespace SchoolManagement.API.Schools;

public static class SchoolsEndpoints
{
    public static void Map(WebApplication app)
    {
        var schoolsGroup = app.MapGroup("/schools")
            .WithTags("Schools group")
            .WithOpenApi();

        schoolsGroup.MapGet("/", GetAllSchoolsHandler.Handle)
            .WithSummary("Get all schools")
            .Produces<SchoolItemDto[]>();

        schoolsGroup.MapGet("/{schoolId:int}", GetSchoolByIdHandler.Handle)
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithSummary("Get school by id")
            .Produces<SchoolDetailsDto>()
            .Produces(StatusCodes.Status404NotFound);

        schoolsGroup.MapPost("/", CreateSchoolHandler.Handle)
            .WithSummary("Create school")
            .Produces<SchoolDetailsDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        schoolsGroup.MapPut("/{schoolId:int}", UpdateSchoolHandler.Handle)
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithSummary("Update school")
            .Produces<SchoolDetailsDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        schoolsGroup.MapDelete("/{schoolId:int}", DeleteSchoolHandler.Handle)
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithSummary("Delete school")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
