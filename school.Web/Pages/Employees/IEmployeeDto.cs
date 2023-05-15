namespace SchoolManagement.Web.Pages.Employees;

public interface IEmployeeDto
{
    int Age { get; init; }
    string FirstName { get; init; }
    string LastName { get; init; }
}