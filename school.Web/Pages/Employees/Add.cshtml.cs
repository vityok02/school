using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees;

public class AddModel : BasePageModel
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPositionRepository _positionRepository;

    public EmployeeDto EmployeeDto { get; set; } = default!;
    public IEnumerable<PositionDto> PositionsDto { get; set; } = null!;

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

    public IActionResult OnPost(string firstName, string lastName, int age, int[] positions)
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

        var employees = _employeeRepository.GetAll(e => e.SchoolId == schoolId);

        if (employees.Any(s => s.FirstName == firstName
                && s.LastName == lastName
                && s.Age == age))
        {
            Message = "Such employee already exists";
            return RedirectToPage("Add");
        }

        var employee = new Employee(firstName, lastName, age)
        {
            School = school
        };

        foreach(var positionId in positions)
        {
            employee.Positions.Add(_positionRepository.Get(positionId)!);
        }

        _employeeRepository.Add(employee);
        return RedirectToPage("List");
    }
}
