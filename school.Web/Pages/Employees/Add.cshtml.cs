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
        var schoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]!);
        var school = _schoolRepository.Get(schoolId);

        Employee? employee = null;
        if(type == "Director")
        {
            employee = new Director(firstName, lastName, age);
        }
        if(type == "Teacher")
        {
            employee = new Teacher(firstName, lastName, age);
        }
        var (valid, error) = school.AddEmployee(employee);
        _employeeRepository.SaveChanges();
        //_employeeRepository.Add(employee!);

        return RedirectToPage("List");
    }
}
