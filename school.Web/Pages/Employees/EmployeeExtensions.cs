using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Employees;

public static class EmployeeExtensions
{
    public static EmployeeDto ToEmployeeDto(this Employee employee)
    {
        return new EmployeeDto(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Age,
            employee.Positions.Select(p => p.Name));
    }
    public static EmployeeItemDto ToEmployeeItemDto(this Employee employee)
    {
        var position = employee.Positions.FirstOrDefault()?.Name;
        position ??= "None";

        return new EmployeeItemDto(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Age,
            position);
    }
}
