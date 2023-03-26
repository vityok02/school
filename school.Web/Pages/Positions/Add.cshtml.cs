using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Positions;

public class AddModel : BasePageModel
{
    public IRepository<Position> _positionRepository;

    public AddModel(IRepository<School> schoolRepository, IRepository<Position> positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost(string name) 
    {
        var schoolId = GetSchoolId();
        if(schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Position position = new(name);

        var positions = _positionRepository.GetAll();
        if(positions.Any(p => p.Name == name))
        {
            Message = "Such position already exists";
            return RedirectToPage("Add");
        }

        _positionRepository.Add(position);
        return RedirectToPage("AllPositions");
    }
}
