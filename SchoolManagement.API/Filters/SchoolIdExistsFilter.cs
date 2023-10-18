using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Filters;

public class SchoolIdExistsFilter : IEndpointFilter
{
    private readonly ISchoolRepository _schoolRepository;

    public SchoolIdExistsFilter(ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext efiContext,
        EndpointFilterDelegate next)
    {
        var routes = efiContext.HttpContext.Request.RouteValues;

        var schoolIdFromRoute = routes["schoolId"]!.ToString();

        _ = int.TryParse(schoolIdFromRoute, out int schoolId);

        var school = await _schoolRepository.GetAsync(schoolId);

        if (school is null)
        {
            return Results.Problem("School not found", statusCode: StatusCodes.Status404NotFound);
        }

        return await next(efiContext);
    }
}
