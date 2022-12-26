using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class EmployeeFormModel : BasePageModel
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<School> _schoolRepository;

    public IEnumerable<School>? Employees { get; set; }
    public string Message { get; private set; } = "";

    public EmployeeFormModel(IRepository<School> schoolRepository, IRepository<Employee> employeeRepository)
    {
        _schoolRepository = schoolRepository;
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnPost(string firstName, string lastName, int age, string type)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var employees = _employeeRepository
            .GetAll(e => e.SchoolId == schoolId
            && e.FirstName == firstName
            && e.LastName == lastName
            && e.Age == age);

        if (employees.Any())
        {
            Message = "Such employee already exists";
            return Page();
        }

        Employee? employee = null;

        if (type == "Director")
        {
            if (employees.Any(e => e.Job == "Director"))
            {
                Message = "Director already exist";
                return Page();
            }

            employee = new Director(firstName, lastName, age);
        }

        if (type == "Teacher")
        {
            employee = new Teacher(firstName, lastName, age);
        }

        var school = _schoolRepository.Get(schoolId);
        employee!.School = school!;

        _employeeRepository.Add(employee);
        return RedirectToPage("List");
    }
}
