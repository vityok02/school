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
        if (SelectedSchoolId == -1)
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

        var floors = await _floorRepository.GetAllAsync(f => f.SchoolId == SelectedSchoolId);
        FloorDtos = floors.Select(f => f.ToFloorDto()).ToArray();

        return Page();
    }
    public async Task<IActionResult> OnPostAsync(EditRoomDto roomDto, RoomType[] roomTypes)
    {
        var room = await _roomRepository.GetAsync(roomDto.Id);
        var floor = await _floorRepository.GetAsync(roomDto.FloorId);

        if(room is null || floor is null)
        {
            return RedirectToPage("List");
        }

        var rooms = await _roomRepository.GetRoomsAsync(SelectedSchoolId);
        if(rooms.Any(r => r.Number == roomDto.Number
            && r.Id != roomDto.Id))
        {
            FloorDtos = await GetFloorDtosAsync();
            ErrorMessage = "Such room already exists";
            return Page();
        }

        if(!roomTypes.Any())
        {
            FloorDtos = await GetFloorDtosAsync();
            ErrorMessage = "Please select a room type";
            return Page();
        }

        room!.Number = roomDto.Number;
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
    
    private async Task<IEnumerable<FloorDto>> GetFloorDtosAsync()
    {
        var floors = await _floorRepository.GetAllAsync(f => f.SchoolId == SelectedSchoolId);
        return floors.Select(f => f.ToFloorDto()).ToArray();
    }
}
