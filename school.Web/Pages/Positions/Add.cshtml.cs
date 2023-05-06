using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Positions;

public class AddModel : BasePageModel
{
    public IRepository<Position> _positionRepository;

    public string? Name { get; private set; } = null!;

    public AddModel(ISchoolRepository schoolRepository, IRepository<Position> positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<IActionResult> OnPostAsync(string name) 
    {
        if (name is null)
        {
            ErrorMessage = "Enter the job title";
            return Page();
        }

        if(SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var positions = await _positionRepository.GetAllAsync();
        if(positions.Any(p => p.Name == name))
        {
            ErrorMessage = "Such position already exists";
            return Page();
        }

        Position position = new(name);

        await _positionRepository.AddAsync(position);
        return RedirectToPage("AllPositions");
    }
}
