using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Web.Pages.Rooms;

public class BaseRoomPageModel : BasePageModel
{
    protected readonly IFloorRepository _floorRepository;
    protected readonly IRoomRepository _roomRepository;
    protected readonly IValidator<IRoomDto> _validator;

    public IEnumerable<FloorDto>? FloorDtos { get; set; } = default!;
    public string NotFoundMessage { get; set; } = "";

    public BaseRoomPageModel(
        ISchoolRepository schoolRepository,
        IFloorRepository floorRepository,
        IRoomRepository roomRepository,
        IValidator<IRoomDto> validator = null!)
        : base(schoolRepository)
    {
        _floorRepository = floorRepository;
        _roomRepository = roomRepository;
        _validator = validator;
    }

    protected async Task<IEnumerable<FloorDto>> GetFloorsAsync()
    {
        var floors = await _floorRepository.GetSchoolFloorsAsync(SelectedSchoolId);
        return floors.Select(f => f.ToFloorDto()).ToArray();
    }

    protected RoomType GetRoomType(RoomType[] roomTypes)
    {
        RoomType roomType = 0;
        foreach (var rt in roomTypes)
        {
            roomType |= rt;
        }

        return roomType;
    }

    protected async Task<bool> HasFloor()
    {
        var floors = await _floorRepository.GetSchoolFloorsAsync(SelectedSchoolId);
        return floors.Any();
    }

    protected IActionResult RedirectToFloorList()
    {
        return RedirectToPage("/Floors/Add", new {isError = true});
    }
}
