using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class FloorListModel : BasePageModel
{
    private readonly IRepository<Floor> _floorRepository;
    private readonly IRepository<School> _schoolRepository;

    public IEnumerable<Floor>? Floors { get; private set; }

    public FloorListModel(IRepository<Floor> floorRepository, IRepository<School> schoolRepository)
    {
        _floorRepository = floorRepository;
        _schoolRepository = schoolRepository;
    }

    public IActionResult OnGet()
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Floors = _floorRepository!.GetAll(f => f.SchoolId == schoolId);
        return Page();
    }

    public IActionResult OnPostAddFloor()
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Floors = _floorRepository!.GetAll(f => f.SchoolId == schoolId);

        var school = _schoolRepository.Get(schoolId);
        int number;

        if (!Floors!.Any() || Floors!.Last().Number < 0)
            number = 0;
        else
            number = Floors!.Last().Number;

        Floor floor = new(number + 1);

        floor.School = school!;

        _floorRepository!.Add(floor);
        return RedirectToPage("List");
    }
    public IActionResult OnPostAddBasement()
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Floors = _floorRepository!.GetAll(f => f.SchoolId == schoolId);

        var school = _schoolRepository.Get(schoolId);
        int number;

        if (!Floors!.Any() || Floors!.First().Number >= 0)
            number = 0;
        else
            number = Floors!.First().Number;

        Floor floor = new(number - 1);

        floor.School = school!;

        _floorRepository!.Add(floor);
        return RedirectToPage("List");
    }

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