using SchoolManagement.Models;

namespace SchoolManagement.API.Employees.Dtos;

public static class EmployeeExtensions
{
    public static EmployeeDto ToEmployeeDto(this Employee employee)
    {
        return new EmployeeDto(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Age,
            employee.Positions.Select(p => p.Name).ToArray());
    }
}
