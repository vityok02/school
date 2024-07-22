using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Employees.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Employees.Handlers;

public static class CreateEmployeeHandler
{
    public static async Task<IResult> Handle(
        HttpContext context,
        LinkGenerator linkGenerator,
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

        if (await employeeRepository.AnyAsync(
            e => e.SchoolId == schoolId
            && e.FirstName == employeeDto.FirstName
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

        var uri = linkGenerator.GetUriByName(context, "Get employee by id", new
        {
            employeeId = employee.Id,
        });

        var createdEmployeeDto = employee.ToEmployeeDto();
        return Results.Created(uri, createdEmployeeDto);
    }
}
