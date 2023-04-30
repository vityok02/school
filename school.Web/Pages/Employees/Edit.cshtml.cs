using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Web.Pages.Positions;

namespace SchoolManagement.Web.Pages.Employees;

public class EditModel : BasePageModel
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPositionRepository _positionRepository;

    public EmployeeDto EmployeeDto { get; private set; } = default!;
    public IEnumerable<PositionDto>? PositionsDto { get; private set; } = default!;

    public EditModel(
        ISchoolRepository schoolRepository,
        IEmployeeRepository employeeRepository,
        IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
        _positionRepository = positionRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var schoolId = GetSchoolId();
        if(schoolId == -1)
        {
            RedirectToSchoolList();
        }

        var employee = await _employeeRepository.GetEmployeeAsync(id);
        if (employee is null)
        {
            return RedirectToPage("List");
        }

        EmployeeDto = employee.ToEmployeeDto();

        var positions = await _positionRepository.GetSchoolPositionsAsync(schoolId);
        PositionsDto = positions.Select(p => p.ToPositionDto()).ToArray();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(EditEmployeeDto employeeDto, int[] checkedPositionsId)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var employee = await _employeeRepository.GetEmployeeAsync(employeeDto.Id);

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

        foreach(var p in checkedPositionsId)
        {
            var position = await _positionRepository.GetAsync(p);
            employee!.Positions.Add(position!);
        }

        if(!employee!.Positions.Any())
        {
            var data = GetDataAsync();

            return Page();
        }

        await _employeeRepository.UpdateAsync(employee);
        return RedirectToPage("List");

        async Task<(EmployeeDto, IEnumerable<PositionDto>)> GetDataAsync()
        {
            EmployeeDto = employee!.ToEmployeeDto();

            var positions = await _positionRepository.GetSchoolPositionsAsync(schoolId);
            PositionsDto = positions.Select(p => p.ToPositionDto()).ToArray();
            return (EmployeeDto, PositionsDto);
        }
    }

}
