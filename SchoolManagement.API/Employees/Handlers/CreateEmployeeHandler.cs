using SchoolManagement.API.Employees.Dtos;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.API.Employees.Handlers;

public static class CreateEmployeeHandler
{
    public static async Task<IResult> Handle(
        IEmployeeRepository employeeRepository,
        IPositionRepository positionRepository,
        [FromRoute] int schoolId,
        [FromBody] EmployeeCreateDto employeeDto)
    {
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
