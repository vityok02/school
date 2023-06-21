using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class AddModel : BaseRoomPageModel
{
    public AddRoomDto RoomDto { get; set; } = default!;

    public AddModel(
        ISchoolRepository schoolRepository,
        IFloorRepository floorRepository,
        IRoomRepository roomRepository,
        IValidator<IRoomDto> validator)
        : base(schoolRepository, floorRepository, roomRepository, validator)
    { }

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
        var floors = await GetFloorsAsync();

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

            NotFoundMessage = "Such floor doesn't exist";
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
