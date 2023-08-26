namespace SchoolManagement.API.Employees.Dtos;

public record EmployeeEditDto(
    int Id,
    string FirstName,
    string LastName,
    int Age,
    int[] PositionIds);
