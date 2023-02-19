using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class EditModel : BasePageModel
{
    private readonly IRepository<Employee> _employeeRepository;

    public Employee? Employee { get; set; }

    public EditModel(IRepository<School> schoolRepository, IRepository<Employee> employeeRepository)
        :base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnGet(int id)
    {
        Employee = _employeeRepository.Get(id)!;

        return Employee is null ? RedirectToPage("List") : Page();
    }

    public IActionResult OnPost(int id, string firstName, string lastName, int age)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var employee = _employeeRepository.Get(id);
        if (employee is null)
        {
            return RedirectToPage("List");
        }

        //var employees = _employeeRepository.GetAll(e => e.SchoolId == schoolId);
        //if (employees.Where(e => e.FirstName == firstName
        //    && e.LastName == lastName
        //    && e.Age == age).Count() > 1)
        //{
        //    ErrorMessage = "Such employee already exists";
        //}

        employee.UpdateInfo(firstName, lastName, age);

        _employeeRepository.Update(employee);
        return RedirectToPage("List");
    }
}
