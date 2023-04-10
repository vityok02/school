using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class EditModel : BasePageModel
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPositionRepository _positionRepository;

    public EmployeeDto EmployeeDto { get; private set; } = default!;
    [BindProperty]
    public IEnumerable<PositionDto>? PositionsDto { get; private set; } = default!;

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

    public IActionResult OnPost(int id, EmployeeDto employeeDto, int[] positionsId)
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
            return RedirectToPage("List");
        }

        //if (!ModelState.IsValid)
        //{
        //    return Page();

        //    var positions = _positionRepository.GetSchoolPositions(schoolId);
        //    PositionsDto = positions.Select(p => p.ToPositionDto()).ToArray();
        //}

        employee!.UpdateInfo(employeeDto.FirstName, employeeDto.LastName, employee.Age);

        employee.Positions.Clear();
        _employeeRepository.SaveChanges();

        foreach(var p in positionsId)
        {
            employee!.Positions.Add(_positionRepository.Get(p)!);
        }

        if(!employee!.Positions.Any())
        {
            var data = GetData();

            return Page();
        }

        _employeeRepository.Update(employee);
        return RedirectToPage("List");

        (EmployeeDto, IEnumerable<PositionDto>) GetData()
        {
            EmployeeDto = employee!.ToEmployeeDto();

            var positions = _positionRepository.GetSchoolPositions(schoolId);
            PositionsDto = positions.Select(p => p.ToPositionDto()).ToArray();
            return (EmployeeDto, PositionsDto);
        }
    }

}
