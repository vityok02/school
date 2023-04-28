using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class AddModel : BasePageModel
{
    private readonly IRepository<Floor> _floorRepository;

    public int FloorNumber { get; private set; }

    public AddModel(ISchoolRepository schoolRepository, IRepository<Floor> floorRepository)
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
            FloorNumber = 1;
        }
        else
        {
            FloorNumber = floors.Last().Number + 1;
        }

        return Page();
    }

    public IActionResult OnPost(int floorNumber, string type)
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

        var floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);

        if (type == "basement")
        {
            floorNumber = -floorNumber;
        }

        if(floors.Any(f => f.Number == floorNumber))
        {
            ErrorMessage = "Such floor already exists";
            return Page();
        }

        Floor floor = new(floorNumber)
        {
            School = school
        };

        _floorRepository.Add(floor);
        return RedirectToPage("List");
    }
}
