namespace SchoolManagement.API.Features.Employees.Dtos;

public record EmployeeCreateDto(
    string FirstName,
    string LastName,
    int Age,
    int[] PositionIds)
    : IEmployeeDto;