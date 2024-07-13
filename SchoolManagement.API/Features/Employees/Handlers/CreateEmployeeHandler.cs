using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using SchoolManagement.API.Features.Employees;
using SchoolManagement.API.Features.Employees.Dtos;

namespace SchoolManagement.API.Features.Employees.Handlers;

public static class CreateEmployeeHandler
{
    public static async Task<IResult> Handle(
        IValidator<IEmployeeDto> validator,
        IEmployeeRepository employeeRepository,
        IPositionRepository positionRepository,
        [FromRoute] int schoolId,
        [FromBody] EmployeeCreateDto employeeDto)
    {
        var validationResult = await validator.ValidateAsync(employeeDto);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var employees = await employeeRepository.GetAllAsync(e => e.SchoolId == schoolId);

        if (employees.Any(
            e => e.FirstName == employeeDto.FirstName
            && e.LastName == employeeDto.LastName
            && e.Age == employeeDto.Age))
        {
            return Results.Conflict(EmployeeErrorMessages.Dublicate);
        }

        var employee = new Employee(employeeDto.FirstName!, employeeDto.LastName!, employeeDto.Age)
        {
            Positions = (ICollection<Position>)await positionRepository
                .GetEmployeePositions(employeeDto.PositionIds),
            SchoolId = schoolId
        };

        await employeeRepository.AddAsync(employee);

        var createdEmployeeDto = employee.ToEmployeeDto();
        return Results.Created($"/schools/{schoolId}/employees/{employee.Id}", createdEmployeeDto);
    }
}
