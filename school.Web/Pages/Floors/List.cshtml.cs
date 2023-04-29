using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchoolManagement.Web.Pages.Floors;

public class FloorListModel : BasePageModel
{
    private readonly IFloorRepository _floorRepository;

    public IEnumerable<FloorItemDto> FloorsDto { get; private set; } = null!;

    public FloorListModel(ISchoolRepository schoolRepository, IFloorRepository floorRepository)
        : base(schoolRepository)
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

        var school = SchoolRepository.Get(schoolId);
        if(school is null)
        {
            return RedirectToSchoolList();
        }

        var floors = _floorRepository.GetFloors(schoolId);
        FloorsDto = floors.Select(f => f.ToFloorItemDto()).ToArray();

        return Page();
    }

    //public IActionResult OnPostAddFloor()
    //{
    //    var schoolId = GetSchoolId();
    //    if (schoolId == -1)
    //    {
    //        return RedirectToSchoolList();
    //    }

    //    FloorsDto = _floorRepository!.GetAll(f => f.SchoolId == schoolId);

    //    var school = SchoolRepository.Get(schoolId);
    //    int number;

    //    if (!FloorsDto!.Any() || FloorsDto!.Last().Number < 0)
    //    {
    //        number = 0;
    //    }
    //    else
    //    {
    //        number = FloorsDto!.Last().Number;
    //    }

    //    Floor floor = new(number + 1)
    //    {
    //        School = school!
    //    };

    //    _floorRepository!.Add(floor);
    //    return RedirectToPage("List");
    //}
    //public IActionResult OnPostAddBasement()
    //{
    //    var schoolId = GetSchoolId();
    //    if (schoolId == -1)
    //    {
    //        return RedirectToSchoolList();
    //    }

    //    Floors = _floorRepository!.GetAll(f => f.SchoolId == schoolId);

    //    var school = SchoolRepository.Get(schoolId);
    //    int number;

    //    if (!Floors!.Any() || Floors!.First().Number >= 0)
    //        number = 0;
    //    else
    //        number = Floors!.First().Number;

    //    Floor floor = new(number - 1);

    //    floor.School = school!;

    //    _floorRepository!.Add(floor);
    //    return RedirectToPage("List");
    //}

    public IActionResult OnPostDelete(int id)
    {
        var floor = _floorRepository!.Get(id);

        if (floor is null)
        {
            return RedirectToPage("List");
        }

        _floorRepository.Delete(floor!);
        return RedirectToPage("List");
    }
}