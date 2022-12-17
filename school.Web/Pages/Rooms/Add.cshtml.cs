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
    public RoomFormModel(IRepository<School> schoolRepository, AppDbContext db, IRepository<Floor> floorRepository, IRepository<Room> roomRepository)
    {
        _schoolRepository = schoolRepository;
        _floorRepository = floorRepository;
        _roomRepository = roomRepository;
    }
    public void OnGet()
    {
        var schoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]!);
        Schools = _schoolRepository.GetAll();
        Floors = _floorRepository.GetAll().Where(f => f.SchoolId == schoolId);
    }
    public IActionResult OnPost(int id, int roomNumber, int floorNumber, RoomType roomType)
    {
        var SchoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]!);
        var floor = _floorRepository.GetAll()
            .Where(f => f.SchoolId == SchoolId && f.Number == floorNumber)
            .SingleOrDefault();

        _roomRepository.Add(new Room(roomNumber, roomType, floor!));
        return RedirectToPage("List");
    }
}
