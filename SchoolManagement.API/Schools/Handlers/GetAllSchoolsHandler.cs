using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Schools.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Schools.Handlers;

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
        var schools = await repository.GetSchools(searchTerm, sortColumn, sortOrder, page, pageSize);

        var schoolItemDtos = schools.Select(s => s.ToSchoolItemDto());
        return Results.Ok(schoolItemDtos);
    }
}
