using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Schools.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Schools.Handlers;

public static class GetAllSchoolsHandler
{
    public static async Task<IResult> Handle(
        ISchoolRepository repository,
        [FromQuery] string? searchTerm,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var schoolsQuery = repository
            .GetSchoolsQuery(searchTerm, sortColumn, sortOrder)
            .Select(s => s.ToSchoolDto());

        var schools = await PagedList<SchoolDto>
            .CreateAsync(schoolsQuery, page, pageSize);

        return Results.Ok(schools);
    }
}
