using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Positions;

public class EditModel : BasePositionPageModel
{
    public PositionDto PositionDto { get; private set; } = default!;

    public EditModel(
        ISchoolRepository schoolRepository, 
        IPositionRepository positionRepository,
        IValidator<PositionDto> validator)
        : base(schoolRepository, positionRepository, validator)
    { }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var position = await _positionRepository.GetAsync(id);
        if (position == null)
        {
            return RedirectToPage("AllPositions");
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
            return RedirectToPage("AllPositions");
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

        return RedirectToPage("AllPositions");
    }
}
