using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class EmployeeFormModel : BasePageModel
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeFormModel(IRepository<School> schoolRepository, IEmployeeRepository employeeRepository)
        : base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnPost(string firstName, string lastName, int age, string type)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var school = SchoolRepository.Get(schoolId);
        if(school is null)
        {
            return RedirectToSchoolList();
        }

        var employees = _employeeRepository.GetSchoolEmployees(schoolId, null!, 0, null!);

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

            var director = employee as Director;
            director!.School = school;
        }

        if (type == "Teacher")
        {
            employee = new Teacher(firstName, lastName, age);
            var teacher = employee as Teacher;
            teacher!.School = school;
        }

        //if (employees
        //    .Any(e => e.FirstName == employee.FirstName
        //        && e.LastName == employee.LastName
        //        && e.Age == employee.Age
        //        && e.);

        //employee!.School = school!;
        _employeeRepository.Add(employee);
        //school.AddEmployee(employee);
        return RedirectToPage("List");
    }
}
