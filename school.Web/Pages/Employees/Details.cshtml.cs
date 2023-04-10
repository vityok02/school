using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class DetailsModel : BasePageModel
{
    private readonly IRepository<Employee> _employeeRepository;

    public EmployeeDto EmployeeDto { get; private set; } = default!;

    public DetailsModel(ISchoolRepository schoolRepository, IRepository<Employee> employeeRepository)
        : base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnGet(int id)
    {
        var employee = _employeeRepository.Get(id);
        EmployeeDto = employee!.ToEmployeeDto();

        return EmployeeDto is null ? RedirectToPage("List") : Page();
    }
}