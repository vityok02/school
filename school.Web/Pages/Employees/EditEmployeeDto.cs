using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Employees;

public record EditEmployeeDto (int Id, string FirstName, string LastName, int Age, IEnumerable<Position> Positions);
