using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Positions;

public class EditModel : BasePageModel
{
    private readonly IPositionRepository _positionRepository;

    public PositionDto? PositionDto { get; private set; } = null!;

    public EditModel(ISchoolRepository schoolRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

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
            return Page();
        }

        position.Name = positionDto.Name;

        await _positionRepository.UpdateAsync(position);

        return RedirectToPage("AllPositions");
    }
}
