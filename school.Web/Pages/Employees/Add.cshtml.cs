using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Web.Pages.Positions;
using SchoolManagement.Web.Pages;

namespace SchoolManagement.Web.Pages.Employees;

public class AddModel : BaseEmployeePageModel
{
    public AddEmployeeDto EmployeeDto { get; private set; } = default!;
    public IEnumerable<int> CheckedPositionsId { get; private set; } = default!;

    public AddModel(
        ISchoolRepository schoolRepository,
        IEmployeeRepository employeeRepository,
        IPositionRepository positionRepository,
        IValidator<IEmployeeDto> validator)
        : base(schoolRepository, employeeRepository, positionRepository, validator)
    {
        _employeeRepository = employeeRepository;
        _positionRepository = positionRepository;
        _validator = validator;
    }

    public async Task<IActionResult> OnGetAsync()
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

        var positions = await _positionRepository.GetSchoolPositionsAsync(SelectedSchoolId);
        PositionDtos = positions.Select(s => s.ToPositionDto()).ToArray();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(AddEmployeeDto employeeDto, int[] checkedPositionsId)
    {
        var validationResult = await _validator.ValidateAsync(employeeDto);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, nameof(EmployeeDto));

            await FillDataForPage();

            return Page();
        }

        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var positions = await _positionRepository.GetSchoolPositionsAsync(SelectedSchoolId);

        foreach (var positionId in checkedPositionsId) 
        {
            if(!positions.Any(p => p.Id == positionId))
            {
                await FillDataForPage();

                InValidPositionMessage = "Such position doesn't exist";

                return Page();
            }
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
            await FillDataForPage();

            ErrorMessage = "Such employee already exists";

            return Page();
        }

        if (!checkedPositionsId.Any())
        {
            await FillDataForPage();

            InValidPositionMessage = "Please select a position";

            return Page();
        }

        var employee = new Employee(employeeDto.FirstName, employeeDto.LastName, employeeDto.Age)
        {
            School = school,
            Positions = (ICollection<Position>)employeePositions,
        };

        await _employeeRepository.AddAsync(employee);
        return RedirectToPage("List");

        async Task FillDataForPage()
        {
            var positions = await _positionRepository.GetSchoolPositionsAsync(SelectedSchoolId);
            PositionDtos = positions.Select(p => p.ToPositionDto()).ToArray();
            CheckedPositionsId = checkedPositionsId;
        }
    }
}
