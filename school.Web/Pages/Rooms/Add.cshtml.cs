using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class RoomFormModel : BasePageModel
{
    private readonly IRepository<Floor> _floorRepository;
    private readonly IRepository<Room> _roomRepository;

    public IEnumerable<Floor>? Floors { get; set; } = null!;


    public RoomFormModel(ISchoolRepository schoolRepository, IRepository<Floor> floorRepository, IRepository<Room> roomRepository)
        :base(schoolRepository)
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
            ErrorMessage = "A room with this number already exists";
            return OnGet();
        }

        var floor = _floorRepository.GetAll(f => f.SchoolId == schoolId && f.Number == floorNumber).SingleOrDefault();

        RoomType roomType = RoomHelper.GetRoomType(roomTypes);

        if (roomType == 0)
        {
            ErrorMessage = "Choose room type";
            return OnGet();
        }

        _roomRepository.Add(new Room(roomNumber, roomType, floor!));
        return RedirectToPage("List");
    }
}
