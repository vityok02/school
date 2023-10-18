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
        var employee = await repository.GetSchoolEmployeesWithPositionsAsync(schoolId, employeeId);

        if (employee is null)
        {
            return Results.NotFound("No such employee found");
        }

        var employeeDto = employee.ToEmployeeDto();
        return Results.Ok(employeeDto);
    }
}
