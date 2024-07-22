using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Employees;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Employees.Handlers;

public static class DeleteEmployeeHandler
{
    public static async Task<IResult> Handle(
        IEmployeeRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int employeeId)
    {
        var employee = await repository.GetByIdAsync(employeeId);

        if (employee is null || employee.SchoolId != schoolId)
        {
            return Results.NotFound(EmployeeErrorMessages.NotFound);
        }

        await repository.DeleteAsync(employee!);
        return Results.NoContent();
    }
}
