namespace SchoolManagement.API.Employees.Dtos;

public record EmployeeCreateDto(
    string FirstName, 
    string LastName, 
    int Age, 
    int[] PositionIds)
    : IEmployeeDto;