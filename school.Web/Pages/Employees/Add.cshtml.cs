using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Data;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Web.Pages.Employees;

public class EmployeeFormModel : PageModel
{
    public readonly IRepository<Employee> _employeeRepository;
    public readonly IRepository<School> _schoolRepository;
    public IEnumerable<School>? Employees { get; private set; }
    public string Message { get; set; } = "";
    public EmployeeFormModel(IRepository<School> schoolRepository, IRepository<Employee> employeeRepository)
    {
        _schoolRepository = schoolRepository;
        _employeeRepository = employeeRepository;
    }
    public void OnGet()
    {
    }
    public IActionResult OnPost(string firstName, string lastName, int age, string type)
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        if(sId is null || !int.TryParse(sId, out int schoolId))
        {
            return NotFound("School not found");
        }

        var school = _schoolRepository.Get(schoolId);
        var employees = _employeeRepository.GetAll(e => e.SchoolId == schoolId);

        Employee? employee = null;

        if (employees.Any(e => e.FirstName == firstName 
            && e.LastName == lastName
            && e.Age == age))
        {
            Message = "Such employee already exists";
            return Page();
        }

        if(type == "Director")
        {
            if(employees.Any(e => e.Job == "Director"))
            {
                Message = "Director already exist";
                return Page();
            }

            employee = new Director(firstName, lastName, age);
        }

        if(type == "Teacher")
        {
            employee = new Teacher(firstName, lastName, age);
        }

        employee!.School = school!;

        _employeeRepository.Add(employee);

        return RedirectToPage("List");
    }
}
