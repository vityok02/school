using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class EditModel : BasePageModel
{
    private readonly IRepository<Room> _roomRepository;
    private readonly IRepository<Floor> _floorRepository;

    [BindProperty]
    public Room Room { get; set; } = default!;
    public IEnumerable<Floor> Floors { get; set; } = default!;

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

        var room = _roomRepository.Get(id);
        if (room is null)
        {
            return RedirectToPage("List");
        }

        Room = room;

        Floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);
        return Page();
    }
    public IActionResult OnPost(Room room, RoomType[] roomTypes)
    {
        var floor = _floorRepository.Get(room.FloorId);
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
