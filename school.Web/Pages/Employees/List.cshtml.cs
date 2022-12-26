using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class ListModel : BasePageModel
{
    private readonly IRepository<Employee> _employeeRepository;

    public static IEnumerable<Employee>? Employees { get; private set; }
    public string Message { get; private set; } = "";

    public ListModel(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnGet()
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Employees = _employeeRepository.GetAll(e => e.SchoolId == schoolId);

        return Page();
    }

    public IActionResult OnPostDelete(int id)
    {
        var employee = _employeeRepository.Get(id);
        if (employee is null)
        {
            return RedirectToPage("List");
        }

        _employeeRepository.Delete(employee!);

        return RedirectToPage("List");
    }
}