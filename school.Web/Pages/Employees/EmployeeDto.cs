using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Employees
{
    public record EmployeeDto(
        int Id,
        string FirstName,
        string LastLame,
        int Age,
        IEnumerable<Position> Positions);
}
