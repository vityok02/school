using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Positions;

public class EditModel : BasePageModel
{
    private readonly IPositionRepository _positionRepository;

    public Position? Position { get; set; } = null!;

    public EditModel(ISchoolRepository schoolRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public IActionResult OnGet(int id)
    {
        Position = _positionRepository.Get(id);
        if (Position == null)
        {
            return RedirectToPage("AllPositions");
        }
        return Page();
    }

    public IActionResult OnPost(int id, string name)
    { 
        var position = _positionRepository.Get(id);
        if (position == null)
        {
            return RedirectToPage("AllPositions");
        }
        position!.Name = name;
        _positionRepository.Update(position);

        return RedirectToPage("AllPositions");
    }
}
