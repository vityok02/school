namespace SchoolManagement.API.Employees.Dtos;

public record EmployeeAddDto(
    string FirstName, 
    string LastName, 
    int Age, 
    int[] PositionIds);