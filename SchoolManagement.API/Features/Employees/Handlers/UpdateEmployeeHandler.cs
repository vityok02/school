using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Employees.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Employees.Handlers;

public static class UpdateEmployeeHandler
{
    public static async Task<IResult> Handle(
        IValidator<IEmployeeDto> validator,
        IEmployeeRepository employeeRepository,
        [FromRoute] int schoolId,
        [FromRoute] int employeeId,
        [FromBody] EmployeeUpdateDto employeeDto)
    {
        var validationResult = await validator.ValidateAsync(employeeDto);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (await employeeRepository.AnyAsync(
            e => e.SchoolId == schoolId
            && e.Id != employeeDto.Id
            && e.FirstName == employeeDto.FirstName
            && e.LastName == employeeDto.LastName
            && e.Age == employeeDto.Age))
        {
            return Results.Conflict(EmployeeErrorMessages.Dublicate);
        }

        var employee = await employeeRepository.GetByIdAsync(employeeId);

        if (employee is null || employee.SchoolId != schoolId)
        {
            return Results.NotFound(EmployeeErrorMessages.NotFound);
        }

        employee!.UpdateInfo(employeeDto.FirstName, employeeDto.LastName, employeeDto.Age);

        await employeeRepository.UpdateAsync(employee);

        var updatedEmployeeDto = employee.ToEmployeeDto();
        return Results.Ok(updatedEmployeeDto);
    }
}
