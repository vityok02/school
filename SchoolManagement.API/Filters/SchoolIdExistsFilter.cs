﻿using SchoolManagement.API.Schools;
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

        bool exist = await _schoolRepository.DoesSchoolExist(schoolId);

        if (!exist)
        {
            return Results.NotFound(SchoolErrorMessages.NotFound);
        }

        return await next(efiContext);
    }
}
