using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class EditModel : BasePageModel
{
    private readonly IRoomRepository _roomRepository;
    private readonly IFloorRepository _floorRepository;

    public EditRoomDto RoomDto { get; private set; } = default!;
    public RoomType CheckedTypes { get; private set; }
    public IEnumerable<FloorDto> FloorDtos { get; private set; } = default!;

    public EditModel(ISchoolRepository schoolRepository, IRoomRepository roomRepository, IFloorRepository floorRepository)
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

        CheckedTypes = room.Type;

        RoomDto = room.ToEditRoomDto();

        var floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);
        FloorDtos = floors.Select(f => f.ToFloorDto()).ToArray();

        return Page();
    }
    public IActionResult OnPost(EditRoomDto roomDto, RoomType[] roomTypes)
    {
        var room = _roomRepository.Get(roomDto.Id);
        room!.Number = roomDto.Number;

        var floor = _floorRepository.Get(roomDto.FloorId);
        if (floor is null)
        {
            return RedirectToPage("List");
        }

        room.FloorId = roomDto.FloorId;

        room!.Floor = floor;

        RoomType roomType = RoomHelper.GetRoomType(roomTypes);
        foreach (var rt in roomTypes)
        {
            roomType |= rt;
        }

        room.Type = roomType;

        _roomRepository.Update(room!);
        return RedirectToPage("List");
    }
}
