using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Employees.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Employees.Handlers;

public static class GetEmployeeByIdHandler
{
    public static async Task<IResult> Handle(
        IEmployeeRepository repository,
        [FromRoute] int schoolId,
        [FromRoute] int employeeId)
    {
        var employee = await repository.GetEmployeeWithPositionsAsync(employeeId);

        if (employee is null || employee.SchoolId != schoolId)
        {
            return Results.NotFound("No such employee found");
        }

        var employeeDto = employee.ToEmployeeDto();
        return Results.Json(employeeDto);
    }
}
