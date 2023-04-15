using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Web.Pages.Employees;

namespace SchoolManagement.Web.Pages.Positions;

public class AddModel : BasePageModel
{
    public IRepository<Position> _positionRepository;

    public AddingPositionDto? PositionDto { get; private set; } = null!;

    public AddModel(ISchoolRepository schoolRepository, IRepository<Position> positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public IActionResult OnPost(AddingPositionDto positionDto) 
    {
        if (positionDto.Name is null)
        {
            ErrorMessage = "Enter the job title";
            return Page();
        }

        var schoolId = GetSchoolId();
        if(schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var positions = _positionRepository.GetAll();
        if(positions.Any(p => p.Name == positionDto.Name))
        {
            ErrorMessage = "Such position already exists";
            return Page();
        }

        Position position = new(positionDto.Name);

        _positionRepository.Add(position);
        return RedirectToPage("AllPositions");
    }
}
