using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class EditModel : BasePageModel
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
            return RedirectToPage("List");
        }

        return Page();
    }

    public IActionResult OnPost(int id, string firstName, string lastName, int age)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var employees = _employeeRepository.GetAll(e => e.SchoolId == schoolId);
        var employee = _employeeRepository.Get(id);
        if (employee is null)
        {
            return RedirectToPage("List");
        }

        if (employees.Where(e => e.FirstName == firstName
            && e.LastName == lastName
            && e.Age == age).Count() > 1)
        {
            Message = "Such employee already exists";
        }


        employee.UpdateInfo(firstName, lastName, age);

        _employeeRepository.Update(employee);
        return Redirect($"/employees/{id}");
    }
}
