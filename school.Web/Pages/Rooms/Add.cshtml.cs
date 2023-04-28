using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Web.Pages.Floors;

namespace SchoolManagement.Web.Pages.Rooms;

public class AddModel : BasePageModel
{
    private readonly IFloorRepository _floorRepository;
    private readonly IRepository<Room> _roomRepository;

    public IEnumerable<FloorDto>? FloorsDto { get; private set; } = default!;
    public AddRoomDto RoomDto { get; set; }

    public AddModel(ISchoolRepository schoolRepository, IFloorRepository floorRepository, IRepository<Room> roomRepository)
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

        var floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);
        FloorsDto = floors.Select(f => f.ToFloorDto()).ToArray();

        return Page();
    }

    public IActionResult OnPost(AddRoomDto roomDto, RoomType[] roomTypes)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var rooms = _roomRepository.GetAll(r => r.Floor.SchoolId == schoolId);
        if (rooms.Any(r => r.Number == roomDto.Number))
        {
            ErrorMessage = "A room with this number already exists";
            return OnGet();
        }

        var floor = _floorRepository.Get(roomDto.FloorId);

        RoomType roomType = RoomHelper.GetRoomType(roomTypes);

        if (roomType == 0)
        {
            ErrorMessage = "Choose room type";
            return OnGet();
        }

        _roomRepository.Add(new Room(roomDto.Number, roomType, floor!));
        return RedirectToPage("List");
    }
}
