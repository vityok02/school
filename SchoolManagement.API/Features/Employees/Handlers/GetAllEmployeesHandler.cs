using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Employees.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Employees.Handlers;

public static class GetAllEmployeesHandler
{
    public static async Task<IResult> Handle(IEmployeeRepository repository, [FromRoute] int schoolId)
    {
        var employees = await repository.GetAllAsync(e => e.SchoolId == schoolId);

        var employeeItemDtos = employees.Select(e => e.ToEmployeeDto());
        return Results.Ok(employeeItemDtos);
    }
}
