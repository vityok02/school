using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class EmployeeFormModel : BasePageModel
{
    private readonly IRepository<Employee> _employeeRepository;

    public EmployeeFormModel(IRepository<School> schoolRepository, IRepository<Employee> employeeRepository)
        : base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnPost(string firstName, string lastName, int age, string type)
    {
        Schools = GetSchools();

        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var employees = _employeeRepository
            .GetAll(e => e.SchoolId == schoolId);

        if (employees.Any(e => e.FirstName == firstName 
            && e.LastName == lastName
            && e.Age == age))
        {
            ErrorMessage = "Such employee already exists";
            return Page();
        }

        Employee? employee = null;

        if (type == "Director")
        {
            if (employees.Any(e => e.Job == "Director"))
            {
                ErrorMessage = "Director already exist";
                return Page();
            }

            employee = new Director(firstName, lastName, age);
        }

        if (type == "Teacher")
        {
            employee = new Teacher(firstName, lastName, age);
        }

        var school = SchoolRepository.Get(schoolId);
        if(school is null)
        {
            return RedirectToSchoolList();
        }

        employee!.School = school!;

        _employeeRepository.Add(employee);
        return RedirectToPage("List");
    }
}
