using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Employees;
using SchoolManagement.API.Features.Employees.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Employees.Handlers;

public static class GetEmployeeByIdHandler
{
    public static async Task<IResult> Handle(
        IEmployeeRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int employeeId)
    {
        var employee = await repository.GetSchoolEmployee(schoolId, employeeId);

        if (employee is null)
        {
            return Results.NotFound(EmployeeErrorMessages.NotFound);
        }

        var employeeDto = employee.ToEmployeeDto();
        return Results.Ok(employeeDto);
    }
}
