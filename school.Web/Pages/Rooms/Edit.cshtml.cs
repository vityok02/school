using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using static SchoolManagement.Web.Pages.Rooms.RoomHelper;

namespace SchoolManagement.Web.Pages.Rooms;

public class EditRoomModel : BasePageModel
{
    private readonly IFloorRepository _floorRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<IRoomDto> _validator;

    public EditRoomDto RoomDto { get; private set; } = default!;
    public RoomType CheckedTypes { get; private set; }
    public IEnumerable<FloorDto>? FloorDtos { get; set; } = default!;

    public EditRoomModel(
        ISchoolRepository schoolRepository,
        IFloorRepository floorRepository,
        IRoomRepository roomRepository,
        IValidator<IRoomDto> validator)
        : base(schoolRepository)
    {
        _floorRepository = floorRepository;
        _roomRepository = roomRepository;
        _validator = validator;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var room = await _roomRepository.GetByIdAsync(id);
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
        var validationResult = await _validator.ValidateAsync(roomDto);
        var floors = await GetFloorsAsync(_floorRepository, SelectedSchoolId);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, nameof(RoomDto));

            FloorDtos = floors;

            return Page();
        }

        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        if(!floors.Any(f => f.Id == roomDto.FloorId))
        {
            FloorDtos = floors;

            ViewData["NotFoundMessage"] = "Such floor doesn't exist";

            return Page();
        }

        var room = await _roomRepository.GetByIdAsync(roomDto.Id);
        var floor = await _floorRepository.GetByIdAsync(roomDto.FloorId);

        if(room is null || floor is null)
        {
            return RedirectToPage("List");
        }

        var rooms = await _roomRepository.GetRoomsAsync(SelectedSchoolId);

        if(rooms.Any(r => r.Number == roomDto.Number
            && r.Id != roomDto.Id))
        {
            FloorDtos = await GetFloorsAsync(_floorRepository, SelectedSchoolId);

            ErrorMessage = "Such room already exists";

            return Page();
        }

        if(!roomTypes.Any())
        {
            FloorDtos = await GetFloorsAsync(_floorRepository, SelectedSchoolId);

            ErrorMessage = "Please select a room type";

            return Page();
        }

        room!.Number = roomDto.Number;
        room!.Floor = floor;

        RoomType roomType = GetRoomType(roomTypes);
        foreach (var rt in roomTypes)
        {
            roomType |= rt;
        }

        room.Type = roomType;

        await _roomRepository.UpdateAsync(room!);
        return RedirectToPage("List");
    }
}
