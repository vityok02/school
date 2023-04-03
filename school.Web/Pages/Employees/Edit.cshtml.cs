using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class EditModel : BasePageModel
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPositionRepository _positionRepository;

    public Employee? Employee { get; set; } = null!;
    public IEnumerable<Position>? Positions { get; set; } = null!;

    public EditModel(ISchoolRepository schoolRepository, IEmployeeRepository employeeRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
        _positionRepository = positionRepository;
    }

    public IActionResult OnGet(int id)
    {
        Employee = _employeeRepository.GetEmployee(id);

        if (Employee is null)
        {
            return RedirectToPage("List");
        }

        var schoolId = GetSchoolId();
        if(schoolId == -1)
        {
            RedirectToSchoolList();
        }

        Positions = _positionRepository.GetSchoolPositions(schoolId);

        return Page();
    }

    public void OnGetEmptyPositions(int id)
    {

    }

    public IActionResult OnPost(int id, string firstName, string lastName, int age, int[] positions)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var employee = _employeeRepository.GetEmployee(id);
        if (employee is null)
        {
            ModelState.AddModelError("", "Employee not found");
        }

        if (!ModelState.IsValid)
        {
            Employee = employee;
            Positions = _positionRepository.GetSchoolPositions(schoolId);
            return Page();
        }

        employee.UpdateInfo(firstName, lastName, age);

        employee.Positions.Clear();
        _employeeRepository.SaveChanges();

        foreach(var position in positions)
        {
            employee.Positions.Add(_positionRepository.Get(position)!);
        }

        if(!employee.Positions.Any())
        {
            return OnGet(id);
            //display message
        }

        _employeeRepository.Update(employee);
        return RedirectToPage("List");
    }
}
