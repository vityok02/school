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

    public async Task<IActionResult> OnGetAsync()
    {
        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var floors = await _floorRepository.GetAllAsync(f => f.SchoolId == SelectedSchoolId);
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

    public async Task<IActionResult> OnPostAsync(int floorNumber, string type)
    {
        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var school = await SchoolRepository.GetAsync(SelectedSchoolId);
        if (school is null)
        {
            return RedirectToSchoolList();
        }

        var floors = await _floorRepository.GetAllAsync(f => f.SchoolId == SelectedSchoolId);

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

        await _floorRepository.AddAsync(floor);
        return RedirectToPage("List");
    }
}
