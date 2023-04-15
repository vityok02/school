using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Web.Pages.Employees;

namespace SchoolManagement.Web.Pages.Positions;

public class EditModel : BasePageModel
{
    private readonly IPositionRepository _positionRepository;

    public PositionDto? PositionDto { get; set; } = null!;

    public EditModel(ISchoolRepository schoolRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public IActionResult OnGet(int id)
    {
        var position = _positionRepository.Get(id);
        if (position == null)
        {
            return RedirectToPage("AllPositions");
        }

        PositionDto = position.ToPositionDto();

        return Page();
    }

    public IActionResult OnPost(PositionDto positionDto)
    { 
        var position = _positionRepository.Get(positionDto.Id);
        if (position == null)
        {
            return RedirectToPage("AllPositions");
        }

        position.Name = positionDto.Name;

        _positionRepository.Update(position);

        return RedirectToPage("AllPositions");
    }
}
