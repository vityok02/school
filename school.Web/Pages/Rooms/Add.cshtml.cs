using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class AddModel : BasePageModel
{
    private readonly IFloorRepository _floorRepository;
    private readonly IRepository<Room> _roomRepository;

    public IEnumerable<FloorDto>? FloorsDto { get; private set; } = default!;
    public AddRoomDto RoomDto { get; set; } = default!;

    public AddModel(ISchoolRepository schoolRepository, IFloorRepository floorRepository, IRepository<Room> roomRepository)
        :base(schoolRepository)
    {
        _floorRepository = floorRepository;
        _roomRepository = roomRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var floors = await _floorRepository.GetAllAsync(f => f.SchoolId == schoolId);
        FloorsDto = floors.Select(f => f.ToFloorDto()).ToArray();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(AddRoomDto roomDto, RoomType[] roomTypes)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var rooms = await _roomRepository.GetAllAsync(r => r.Floor.SchoolId == schoolId);
        if (rooms.Any(r => r.Number == roomDto.Number))
        {
            ErrorMessage = "A room with this number already exists";
            return Page();
        }

        var floor = await _floorRepository.GetAsync(roomDto.FloorId);

        RoomType roomType = RoomHelper.GetRoomType(roomTypes);

        if (roomType == 0)
        {
            ErrorMessage = "Choose room type";
            return Page();
        }

        await _roomRepository.AddAsync(new Room(roomDto.Number, roomType, floor!));
        return RedirectToPage("List");
    }
}
