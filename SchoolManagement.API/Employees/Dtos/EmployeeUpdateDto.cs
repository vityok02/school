namespace SchoolManagement.API.Employees.Dtos;

public record EmployeeUpdateDto(
    int Id,
    string FirstName,
    string LastName,
    int Age,
    int[] PositionIds);
