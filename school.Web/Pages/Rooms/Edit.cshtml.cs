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

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var room = await _roomRepository.GetAsync(id);
        if (room is null)
        {
            return RedirectToPage("List");
        }

        CheckedTypes = room.Type;

        RoomDto = room.ToEditRoomDto();

        var floors = await _floorRepository.GetAllAsync(f => f.SchoolId == schoolId);
        FloorDtos = floors.Select(f => f.ToFloorDto()).ToArray();

        return Page();
    }
    public async Task<IActionResult> OnPostAsync(EditRoomDto roomDto, RoomType[] roomTypes)
    {
        var room = await _roomRepository.GetAsync(roomDto.Id);
        room!.Number = roomDto.Number;

        var floor = await _floorRepository.GetAsync(roomDto.FloorId);
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

        await _roomRepository.UpdateAsync(room!);
        return RedirectToPage("List");
    }
}
