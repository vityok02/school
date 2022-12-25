using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class RoomFormModel : BasePageModel
{
    private readonly IRepository<Floor> _floorRepository;
    private readonly IRepository<Room> _roomRepository;

    public IEnumerable<School>? Schools { get; private set; }
    public static IEnumerable<Floor>? Floors { get; private set; }
    public string Message { get; private set; } = "";

    public RoomFormModel(IRepository<Floor> floorRepository, IRepository<Room> roomRepository)
    {
        _floorRepository = floorRepository;
        _roomRepository = roomRepository;
    }

    public IActionResult OnGet()
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);
        return Page();
    }

    public IActionResult OnPost(int roomNumber, int floorNumber, RoomType[] roomTypes)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
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

        if (roomType == 0)
        {
            Message = "Choose room type";
            return Page();
        }

        _roomRepository.Add(new Room(roomNumber, roomType, floor!));
        return RedirectToPage("List");
    }
}
