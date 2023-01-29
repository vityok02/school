using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class ListModel : BasePageModel
{
    private readonly IRepository<Employee> _employeeRepository;

    public IEnumerable<Employee> Employees { get; private set; } = null!;

    public ListModel(IRepository<School> schoolRepository, IRepository<Employee> employeeRepository)
        :base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnGet(string orderBy)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Employees = _employeeRepository.GetAll(e => e.SchoolId == schoolId, Sort(orderBy));

        return Page();

        static Func<IQueryable<Employee>, IOrderedQueryable<Employee>> Sort(string orderBy)
        {
            return orderBy switch
            {
                "firstName" => e => e.OrderBy(e => e.FirstName),
                "lastName" => e => e.OrderBy(e => e.LastName),
                "age" => e => e.OrderBy(e => e.Age),
                _ => e => e.OrderBy(e => e.FirstName),
            };
        }
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