using SchoolManagement.API.Features.Schools.Dtos;
using SchoolManagement.API.Features.Schools.Handlers;
using SchoolManagement.API.Filters;

namespace SchoolManagement.API.Features.Schools;

public static class SchoolsEndpoints
{
    public static void Map(WebApplication app)
    {
        var schoolsGroup = app.MapGroup("/schools")
            .WithTags("Schools group")
            .WithOpenApi();

        schoolsGroup.MapGet("/", GetAllSchoolsHandler.Handle)
            .WithSummary("Get all schools")
            .Produces<PagedList<SchoolDto>>();

        schoolsGroup.MapGet("/{schoolId:int}", GetSchoolByIdHandler.Handle)
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithSummary("Get school by id")
            .WithName("SchoolInfo")
            .Produces<SchoolDto>()
            .Produces(StatusCodes.Status404NotFound);

        schoolsGroup.MapPost("/", CreateSchoolHandler.Handle)
            .WithSummary("Create school")
            .Produces<SchoolDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        schoolsGroup.MapPut("/{schoolId:int}", UpdateSchoolHandler.Handle)
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithSummary("Update school")
            .Produces<SchoolDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        schoolsGroup.MapDelete("/{schoolId:int}", DeleteSchoolHandler.Handle)
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithSummary("Delete school")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
