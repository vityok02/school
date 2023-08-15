using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Positions;

public class AddPositionModel : BasePageModel
{
    private readonly IPositionRepository _positionRepository;
    private readonly IValidator<PositionDto> _validator;

    public string? Name { get; private set; } = null!;

    public AddPositionModel(
        ISchoolRepository schoolRepository,
        IPositionRepository positionRepository,
        IValidator<PositionDto> validator)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
        _validator = validator;
    }

    public async Task<IActionResult> OnPostAsync(string name)
    {
        PositionDto positionDto = new(0, name);

        var validationResult = await _validator.ValidateAsync(positionDto);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);

            return Page();
        }

        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var positions = await _positionRepository.GetAllAsync();
        if (positions.Any(p => p.Name == name))
        {
            ErrorMessage = "Such position already exists";
            return Page();
        }

        Position position = new(name);

        await _positionRepository.AddAsync(position);
        return RedirectToPage("/Positions/List");
    }
}
