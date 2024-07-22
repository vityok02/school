using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Employees.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Employees.Handlers;

public static class GetAllEmployeesHandler
{
    public static async Task<IResult> Handle(
        IEmployeeRepository repository,
        [FromRoute] int schoolId,
        [FromQuery] string? searchTerm,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page,
        int pageSize)
    {
        var employeesQuery = repository
            .GetEmployeesQuery(schoolId, searchTerm, sortColumn, sortOrder)
            .Select(e => e.ToEmployeeDto());

        var employees = await PagedList<EmployeeDto>.CreateAsync(employeesQuery, page, pageSize);

        return Results.Ok(employees);
    }
}
