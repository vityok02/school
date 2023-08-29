using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Employees.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Employees.Handlers;

public static class UpdateEmployeeHandler
{
    public static async Task<IResult> Handle(
        IEmployeeRepository employeeRepository,
        [FromRoute] int schoolId,
        [FromRoute] int employeeId,
        [FromBody] EmployeeEditDto employeeDto)
    {
        var employee = await employeeRepository.GetAsync(employeeId);

        if (employee is null || employee.SchoolId != schoolId)
        {
            return Results.NotFound("No such employee found");
        }

        employee!.UpdateInfo(employeeDto.FirstName, employeeDto.LastName, employeeDto.Age);

        await employeeRepository.UpdateAsync(employee);
        return Results.Ok(employee);
    }
}
