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

        //var employees = GetSchoolEmployees();

        //if (employees.Any(e => e.FirstName == firstName
        //    && e.LastName == lastName
        //    && e.Age == age))
        //{
        //    ErrorMessage = "Such employee already exists";
        //    return Page();
        //}

        Employee? employee = null;

        if (type == "Director")
        {
            //if (employees.Any(e => e.Job == "Director"))
            //{
            //    ErrorMessage = "Director already exist";
            //    return Page();
            //}

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

        //employee!.School = school!;

        _employeeRepository.Add(employee);
        return RedirectToPage("List");
    }
}
