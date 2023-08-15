using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Web.Pages.Positions;

namespace SchoolManagement.Web.Pages.Employees;

public class EditModel : BasePageModel
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IValidator<IEmployeeDto> _validator;

    public IEnumerable<PositionDto> PositionDtos { get; protected set; } = default!;

    public EditEmployeeDto EmployeeDto { get; private set; } = default!;

    public EditModel(
        ISchoolRepository schoolRepository,
        IEmployeeRepository employeeRepository,
        IPositionRepository positionRepository,
        IValidator<IEmployeeDto> validator)
        : base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
        _positionRepository = positionRepository;
        _validator = validator;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (SelectedSchoolId == -1)
        {
            RedirectToSchoolList();
        }

        var employee = await _employeeRepository.GetEmployeeAsync(id);
        if (employee is null)
        {
            return RedirectToPage("List");
        }

        EmployeeDto = employee.ToEditEmployeeDto();

        var positions = await _positionRepository.GetSchoolPositions(SelectedSchoolId);
        PositionDtos = positions.Select(p => p.ToPositionDto()).ToArray();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(EditEmployeeDto employeeDto, int[] checkedPositionsId)
    {
        var validationResult = await _validator.ValidateAsync(employeeDto);
        var employee = await _employeeRepository.GetEmployeeAsync(employeeDto.Id);

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

        if (employee is null)
        {
            ModelState.AddModelError("", "EmployeeDto not found");

            return RedirectToPage("List");
        }

        var employees = await _employeeRepository.GetAllAsync(e => e.SchoolId == SelectedSchoolId);

        if (employees
            .Any(e => e.FirstName == employeeDto.FirstName
                && e.LastName == employeeDto.LastName
                && e.Age == employeeDto.Age
                && e.Id != employee.Id))
        {
            await FillDataForPage();

            ErrorMessage = "Such employee already exists";

            return Page();
        }

        var positions = await _positionRepository.GetSchoolPositions(SelectedSchoolId);

        foreach (var positionId in checkedPositionsId)
        {
            if (!positions.Any(p => p.Id == positionId))
            {
                await FillDataForPage();

                ViewData["InValidPositionMessage"] = "Such position doesn't exist";

                return Page();
            }
        }

        employee!.UpdateInfo(employeeDto.FirstName, employeeDto.LastName, employeeDto.Age);

        employee.Positions.Clear();

        foreach (var p in checkedPositionsId)
        {
            var position = await _positionRepository.GetAsync(p);
            employee!.Positions.Add(position!);
        }

        if (!employee!.Positions.Any())
        {
            await FillDataForPage();

            ViewData["InValidPositionMessage"] = "Please select a position";

            return Page();
        }

        await _employeeRepository.UpdateAsync(employee);
        return RedirectToPage("List");

        async Task FillDataForPage()
        {
            EmployeeDto = employee!.ToEditEmployeeDto();

            var positions = await _positionRepository.GetSchoolPositions(SelectedSchoolId);
            PositionDtos = positions.Select(p => p.ToPositionDto()).ToArray();
        }
    }
}
