using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class EditModel : BasePageModel
{
    private readonly IRepository<Room> _roomRepository;
    private readonly IRepository<Floor> _floorRepository;

    [BindProperty]
    public Room? Room { get; set; } = null!;
    public IEnumerable<Floor>? Floors { get; set; } = null!;

    public EditModel(ISchoolRepository schoolRepository, IRepository<Room> roomRepository, IRepository<Floor> floorRepository)
        : base(schoolRepository)
    {
        _roomRepository = roomRepository;
        _floorRepository = floorRepository;
    }

    public IActionResult OnGet(int id)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Room = _roomRepository.Get(id);
        if (Room is null)
        {
            return RedirectToPage("List");
        }

        Floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);
        return Page();
    }
    public IActionResult OnPost(Room room, RoomType[] roomTypes)
    {
        var floor = _floorRepository.Get(room.Floor.Id);
        if (floor is null)
        {
            return RedirectToPage("List");
        }

        Room!.Floor = floor;

        RoomType roomType = RoomHelper.GetRoomType(roomTypes);
        foreach (var rt in roomTypes)
        {
            roomType |= rt;
        }

        Room.Type = roomType;

        _roomRepository.Update(Room!);
        return RedirectToPage("List");
    }
}
