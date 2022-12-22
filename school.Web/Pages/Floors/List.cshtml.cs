using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class FloorListModel : PageModel
{
    private readonly IRepository<Room> _roomsRepository;
    private readonly IRepository<Floor> _floorRepository;
    private readonly IRepository<School> _schoolRepository;
    public School? School { get; set; }
    public IEnumerable<Floor>? Floors { get; private set; }

    public FloorListModel(IRepository<Floor> floorRepository, IRepository<School> schoolRepository, IRepository<Room> roomsRepository)
    {
        _floorRepository = floorRepository;
        _schoolRepository = schoolRepository;
        _roomsRepository = roomsRepository;
    }

    public IActionResult OnGet()
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        if (!int.TryParse(sId, out int schoolId))
        {
            return NotFound("School not found");
        }

        Floors = _floorRepository!.GetAll(f => f.SchoolId == schoolId);
        return Page();
    }

    public void OnPost()
    {

    }

    public IActionResult OnPostAddFloor()
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        if (!int.TryParse(sId, out int schoolId))
        {
            return NotFound("School not found");
        }

        Floors = _floorRepository!.GetAll(f => f.SchoolId == schoolId);

        var school = _schoolRepository.Get(schoolId);
        int number;

        if (!Floors!.Any() || Floors!.Last().Number < 0)
            number = 0;
        else
        {
            number = Floors!.Last().Number;
        }

        Floor floor = new(number + 1);

        floor.School = school!;

        _floorRepository!.Add(floor);
        return RedirectToPage("List");
    }
    public IActionResult OnPostAddBasement()
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        if (!int.TryParse(sId, out int schoolId))
        {
            return NotFound("School not found");
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
            return NotFound("Floor not found");
        }

        _floorRepository.Delete(floor!);
        return RedirectToPage("List");
    }
}
