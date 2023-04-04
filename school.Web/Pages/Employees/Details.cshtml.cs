using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class DetailsModel : BasePageModel
{
    private readonly IRepository<Employee> _employeeRepository;

    public Employee? Employee { get; private set; }

    public DetailsModel(ISchoolRepository schoolRepository, IRepository<Employee> employeeRepository)
        : base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnGet(int id)
    {
        Employee = _employeeRepository.Get(id);

        return Employee is null ? RedirectToPage("List") : Page();
    }
}