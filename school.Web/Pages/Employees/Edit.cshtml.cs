using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class EditModel : PageModel
{
    private readonly IRepository<Employee> _employeeRepository;
    public Employee? Employee { get; set; }
    public string Message { get; set; } = "";
    public EditModel(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnGet(int id)
    {
        Employee = _employeeRepository.Get(id)!;

        if (Employee is null)
        {
            return NotFound("Employee not found");
        }

        return Page();
    }

    public IActionResult OnPost(int id, string firstName, string lastName, int age)
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        if (sId is null || !int.TryParse(sId, out int schoolId))
        {
            return NotFound("School not found");
        }

        var employees = _employeeRepository.GetAll(e => e.SchoolId == schoolId);

        var employee = _employeeRepository.Get(id);

        if(employees.Where(e => e.FirstName == firstName
            && e.LastName == lastName
            && e.Age == age).Count() > 1)
        {
            Message = "Such employee already exists";
        }

        if (employee is null) 
        {
            return NotFound("Employee is not found");
        }

        //if(employees.Any(emp => emp.FirstName == firstName 
        //    && emp.LastName == lastName 
        //    && emp.Age == age))
        //{
        //    Message = "Such employee already exist";
        //    return Page();
        //}

        employee.UpdateInfo(firstName, lastName, age);

        _employeeRepository.Update(employee);
        return Redirect($"/employees/{id}");
    }
}
