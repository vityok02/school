using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using static SchoolManagement.Web.Pages.Rooms.RoomHelper;

namespace SchoolManagement.Web.Pages.Rooms;

public class AddRoomModel : BasePageModel
{
    private readonly IFloorRepository _floorRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<IRoomDto> _validator;

    public AddRoomDto RoomDto { get; set; } = default!;
    public IEnumerable<FloorDto>? FloorDtos { get; set; } = default!;

    public AddRoomModel(
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

        var f = floors.Select(f => f.Id);
        if (!floors.Any(f => f.Id == roomDto.FloorId))
        {
            FloorDtos = floors;

            ViewData["NotFoundMessage"] = "Such floor doesn't exist";
        }

        var rooms = await _roomRepository.GetAllAsync(r => r.Floor.SchoolId == SelectedSchoolId);
        if (rooms.Any(r => r.Number == roomDto.Number))
        {
            FloorDtos = floors;

            ErrorMessage = "A room with this number already exists";

            return Page();
        }

        if (!roomTypes.Any())
        {
            FloorDtos = floors;

            ErrorMessage = "Please select a room type";

            return Page();
        }

        var floor = await _floorRepository.GetAsync(roomDto.FloorId);

        RoomType roomType = GetRoomType(roomTypes);

        await _roomRepository.AddAsync(new Room(roomDto.Number, roomType, floor!));

        return RedirectToPage("List");
    }
}
