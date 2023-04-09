using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Web.Pages.Positions;

namespace SchoolManagement.Web.Pages.Employees;

public class EditModel : BasePageModel
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPositionRepository _positionRepository;

    public EmployeeDto EmployeeDto { get; set; } = default!;
    public ICollection<PositionDto>? PositionsDto { get; set; } = default!;

    public EditModel(ISchoolRepository schoolRepository, IEmployeeRepository employeeRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
        _positionRepository = positionRepository;
    }

    public IActionResult OnGet(int id)
    {
        var employee = _employeeRepository.GetEmployee(id);

        if (employee is null)
        {
            return RedirectToPage("List");
        }

        var schoolId = GetSchoolId();
        if(schoolId == -1)
        {
            RedirectToSchoolList();
        }

        EmployeeDto = employee.ToEmployeeDto();

        var positions = _positionRepository.GetSchoolPositions(schoolId);

        PositionsDto = positions.Select(p => p.ToPositionDto()).ToArray();

        return Page();
    }

    public IActionResult OnPost(int id, EmployeeDto employeeDto, PositionDto positionDto)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var employee = _employeeRepository.GetEmployee(id);

        if (employee is null)
        {
            ModelState.AddModelError("", "EmployeeDto not found");
        }


        if (!ModelState.IsValid)
        {
            employee!.UpdateInfo(employeeDto.FirstName, employeeDto.LastLame, employee.Age);
            var positions = _positionRepository.GetSchoolPositions(schoolId);
            PositionsDto = positions.Select(p => p.ToPositionDto()).ToArray();

            return Page();
        }

        employee!.Positions.Clear();
        _employeeRepository.SaveChanges();

        foreach(var position in PositionsDto)
        {
            ;
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
