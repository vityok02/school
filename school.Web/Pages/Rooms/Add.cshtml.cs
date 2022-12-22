using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class RoomFormModel : PageModel
{
    private readonly IRepository<School> _schoolRepository;
    private readonly IRepository<Floor> _floorRepository;
    private readonly IRepository<Room> _roomRepository;
    public IEnumerable<School>? Schools { get; private set; }
    public static IEnumerable<Floor>? Floors { get; private set; }
    public string Message { get; private set; } = "";
    public RoomFormModel(IRepository<School> schoolRepository, AppDbContext db, IRepository<Floor> floorRepository, IRepository<Room> roomRepository)
    {
        _schoolRepository = schoolRepository;
        _floorRepository = floorRepository;
        _roomRepository = roomRepository;
    }
    public IActionResult OnGet()
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        if (!int.TryParse(sId, out int schoolId))
        {
            return NotFound("School not found");
        }

        Floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);
        return Page();
    }
    public IActionResult OnPost(int id, int roomNumber, int floorNumber, RoomType[] roomTypes)
    {

        var sId = HttpContext.Request.Cookies["SchoolId"];
        if (!int.TryParse(sId, out int schoolId))
        {
            return NotFound("School not found");
        }

        var rooms = _roomRepository.GetAll(r => r.Floor.SchoolId == schoolId);
        if (rooms.Any(r => r.Number == roomNumber))
        {
            Message = "Such room already exist";
            return Page();
        }

        var floor = _floorRepository.GetAll(f => f.SchoolId == schoolId && f.Number == floorNumber).SingleOrDefault();

        RoomType roomType = 0;
        foreach (var rt in roomTypes)
        {
            roomType |= rt;
        }

        _roomRepository.Add(new Room(roomNumber, roomType, floor!));
        return RedirectToPage("List");
    }
}
