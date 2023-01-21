using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class FloorFormModel : BasePageModel
{
    private readonly IRepository<Floor> _floorRepository;

    public int PossibleNumber { get; set; }

    public FloorFormModel(IRepository<School> schoolRepository, IRepository<Floor> floorRepository)
        :base(schoolRepository)
    {
        _floorRepository = floorRepository;
    }

    public IActionResult OnGet()
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);
        if (!floors.Any())
        {
            PossibleNumber = 1;
        }
        else
        {
            PossibleNumber = floors.Last().Number + 1;
        }

        return Page();
    }

    public IActionResult OnPost(int number, string type)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var school = SchoolRepository.Get(schoolId);
        if (school is null)
        {
            return RedirectToSchoolList();
        }

        Floor floor = new(number)
        {
            School = school!
        };

        if (type == "floor")
        {
            floor.Number = number;
        }
        else
        {
            floor.Number = -number;
        }

        _floorRepository.Add(floor);
        return RedirectToPage("List");
    }
}
