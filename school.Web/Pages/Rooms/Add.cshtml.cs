using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class AddModel : BasePageModel
{
    private readonly IFloorRepository _floorRepository;
    private readonly IRepository<Room> _roomRepository;

    public IEnumerable<FloorDto>? FloorDtos { get; set; } = default!;
    public AddRoomDto RoomDto { get; set; } = default!;

    public AddModel(ISchoolRepository schoolRepository, IFloorRepository floorRepository, IRepository<Room> roomRepository)
        :base(schoolRepository)
    {
        _floorRepository = floorRepository;
        _roomRepository = roomRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var floors = await _floorRepository.GetAllAsync(f => f.SchoolId == SelectedSchoolId);
        FloorDtos = floors.Select(f => f.ToFloorDto()).ToArray();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(AddRoomDto roomDto, RoomType[] roomTypes)
    {
        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var rooms = await _roomRepository.GetAllAsync(r => r.Floor.SchoolId == SelectedSchoolId);
        if (rooms.Any(r => r.Number == roomDto.Number))
        {
            FloorDtos = await GetFloorsAsync();

            ErrorMessage = "A room with this number already exists";

            return Page();
        }

        if (!roomTypes.Any())
        {
            FloorDtos = await GetFloorsAsync();

            ErrorMessage = "Please select a room type";

            return Page();
        }

        var floor = await _floorRepository.GetAsync(roomDto.FloorId);

        RoomType roomType = RoomHelper.GetRoomType(roomTypes);

        await _roomRepository.AddAsync(new Room(roomDto.Number, roomType, floor!));
        return RedirectToPage("List");
    }

    private async Task<IEnumerable<FloorDto>> GetFloorsAsync()
    {
        var floors = await _floorRepository.GetSchoolFloorsAsync(SelectedSchoolId);
        return floors.Select(f => f.ToFloorDto()).ToArray();
    }
}
