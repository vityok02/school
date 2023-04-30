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

    public IActionResult OnGet()
    {
        var schoolId = GetSchoolId();
        if(schoolId == -1)
        {
            return RedirectToSchoolList();
        }
        var school = SchoolRepository.Get(schoolId);
        if(school is null)
        {
            return RedirectToSchoolList();
        }

        var positions = _positionRepository.GetSchoolPositions(schoolId);
        PositionsDto = positions.Select(s => s.ToPositionDto()).ToArray();

        return Page();
    }

    public IActionResult OnPost(AddEmployeeDto employeeDto, int[] checkedPositionsId)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var school = SchoolRepository.Get(schoolId);
        if(school is null)
        {
            return RedirectToSchoolList();
        }

        IEnumerable<Position> employeePositions = _positionRepository.GetAll(s => checkedPositionsId.Contains(s.Id)).ToArray();

        var employees = _employeeRepository.GetAll(e => e.SchoolId == schoolId);

        if (employees.Any(s => s.FirstName == employeeDto.FirstName
                && s.LastName == employeeDto.LastName
                && s.Age == employeeDto.Age))
        {
            var positions = _positionRepository.GetSchoolPositions(schoolId);
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

        _employeeRepository.Add(employee);
        return RedirectToPage("List");

    }
}
