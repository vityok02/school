using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Employees;

public static class EmployeeExtensions
{
    public static EditEmployeeDto ToEditEmployeeDto(this Employee employee)
    {
        return new EditEmployeeDto(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Age,
            employee.Positions);
    }

    public static EmployeeDto ToEmployeeDto(this Employee employee)
    {
        var position = employee.Positions.FirstOrDefault()?.Name;
        position ??= "None";

        return new EmployeeDto(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Age,
            position);
    }
}
