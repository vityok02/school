using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace SchoolManagement.Web.Pages.Positions;

public class AddModel : BasePageModel
{
    public IRepository<Position> _positionRepository;

    public AddModel(ISchoolRepository schoolRepository, IRepository<Position> positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost(string name) 
    {
        if (name is null)
        {
            ErrorMessage = "Enter the job title";
            return Page();
        }

        var schoolId = GetSchoolId();
        if(schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Position position = new(name);

        var positions = _positionRepository.GetAll();
        if(positions.Any(p => p.Name == name))
        {
            ErrorMessage = "Such position already exists";
            return Page();
        }

        _positionRepository.Add(position);
        return RedirectToPage("AllPositions");
    }
}
