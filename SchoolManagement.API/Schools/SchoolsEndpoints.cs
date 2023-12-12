using SchoolManagement.Models.Constants;
using SchoolManagement.API.Filters;
using SchoolManagement.API.Schools.Dtos;
using SchoolManagement.API.Schools.Handlers;

namespace SchoolManagement.API.Schools;

public static class SchoolsEndpoints
{
    public static void Map(WebApplication app)
    {
        var manageSchoolsGroup = app.MapGroup("/schools")
            .WithTags("Manage schools group")
            .WithOpenApi()
            .RequireAuthorization(builder =>
                builder.RequireClaim(ClaimNames.Permissions, Permissions.CanManageSchools));

        var schoolsInfoGroup = app.MapGroup("/schools")
            .WithTags("Schools info group")
            .WithOpenApi()
            .RequireAuthorization(Policies.CanViewInfo);

        schoolsInfoGroup.MapGet("/", GetAllSchoolsHandler.Handle)
            .WithSummary("Get all schools")
            .Produces<SchoolItemDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        schoolsInfoGroup.MapGet("/{schoolId:int}", GetSchoolByIdHandler.Handle)
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithSummary("Get school by id")
            .Produces<SchoolDetailsDto>()
            .Produces(StatusCodes.Status404NotFound);

        manageSchoolsGroup.MapPost("/", CreateSchoolHandler.Handle)
            .WithSummary("Create school")
            .Produces<SchoolDetailsDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        manageSchoolsGroup.MapPut("/{schoolId:int}", UpdateSchoolHandler.Handle)
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithSummary("Update school")
            .Produces<SchoolDetailsDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        manageSchoolsGroup.MapDelete("/{schoolId:int}", DeleteSchoolHandler.Handle)
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithSummary("Delete school")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
