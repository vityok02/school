namespace SchoolManagement.API.Features.Employees.Dtos;

public record EmployeeDto(
    int Id,
    string FirstName,
    string LastName,
    int Age,
    string[] Positions);
