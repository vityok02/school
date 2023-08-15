using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Positions;

public class EditPositionModel : BasePageModel
{
    private readonly IPositionRepository _positionRepository;
    private readonly IValidator<PositionDto> _validator;

    public PositionDto PositionDto { get; private set; } = default!;

    public EditPositionModel(ISchoolRepository schoolRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var position = await _positionRepository.GetAsync(id);
        if (position == null)
        {
            return RedirectToPage("/Positions/List");
        }

        PositionDto = position.ToPositionDto();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(PositionDto positionDto)
    {
        var validationResult = await _validator.ValidateAsync(positionDto);
        if(!validationResult.IsValid) 
        {
            validationResult.AddToModelState(ModelState, nameof(PositionDto));

            PositionDto = positionDto;

            return Page();
        }

        var position = await _positionRepository.GetAsync(positionDto.Id);
        if (position == null)
        {
            return RedirectToPage("/Positions/List");
        }

        var positions = await _positionRepository.GetAllAsync();

        if (positions.Any(p => p.Name == positionDto.Name 
            && p.Id != positionDto.Id))
        {
            ErrorMessage = "Such position already exists";

            PositionDto = positionDto;

            return Page();
        }

        position.Name = positionDto.Name;

        await _positionRepository.UpdateAsync(position);

        return RedirectToPage("/Positions/List");
    }
}
