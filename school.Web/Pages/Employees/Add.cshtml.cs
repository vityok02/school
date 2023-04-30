using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Web.Pages.Positions;

namespace SchoolManagement.Web.Pages.Employees;

public class AddModel : BasePageModel
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPositionRepository _positionRepository;

    public AddEmployeeDto EmployeeDto { get; private set; } = default!;
    public IEnumerable<PositionDto>? PositionsDto { get; private set; } = default!;
    public IEnumerable<int> CheckedPositionsId { get; private set; } = default!;

    public AddModel(ISchoolRepository schoolRepository, IEmployeeRepository employeeRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
        _positionRepository = positionRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        //var schoolId = GetSchoolId();
        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var school = await SchoolRepository.GetAsync(SelectedSchoolId);
        if (school is null)
        {
            return RedirectToSchoolList();
        }

        var positions = await _positionRepository.GetSchoolPositionsAsync(SelectedSchoolId);
        PositionsDto = positions.Select(s => s.ToPositionDto()).ToArray();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(AddEmployeeDto employeeDto, int[] checkedPositionsId)
    {
        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var school = await SchoolRepository.GetAsync(SelectedSchoolId);
        if (school is null)
        {
            return RedirectToSchoolList();
        }

        var employeePositions = await _positionRepository.GetEmployeePositions(checkedPositionsId);

        var employees = await _employeeRepository.GetAllAsync(e => e.SchoolId == SelectedSchoolId);

        if (employees.Any(s => s.FirstName == employeeDto.FirstName
                && s.LastName == employeeDto.LastName
                && s.Age == employeeDto.Age))
        {
            var positions = await _positionRepository.GetSchoolPositionsAsync(SelectedSchoolId);
            PositionsDto = positions.Select(p => p.ToPositionDto()).ToArray();
            CheckedPositionsId = checkedPositionsId;

            ErrorMessage = "Such employee already exists";
            return Page();
        }

        var employee = new Employee(employeeDto.FirstName, employeeDto.LastName, employeeDto.Age)
        {
            School = school,
            Positions = (ICollection<Position>)employeePositions,
        };

        await _employeeRepository.AddAsync(employee);
        return RedirectToPage("List");

    }
}
