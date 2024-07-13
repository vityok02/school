using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Schools;
using SchoolManagement.API.Schools.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Schools.Handlers;

public static class GetAllSchoolsHandler
{
    public static async Task<IResult> Handle(
        ISchoolService schoolService,
        [FromQuery] string? searchTerm,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var schools = schoolService.GetSchools(searchTerm, sortColumn, sortOrder, page, pageSize);

        return Results.Ok(schools);
    }
}
