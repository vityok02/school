using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class FloorDetails : BasePageModel
{
    private readonly IFloorRepository _floorRepository;

    public FloorItemDto? FloorDto { get; private set; }

    public FloorDetails(ISchoolRepository schoolRepository, IFloorRepository floorRepository)
        : base(schoolRepository)
    {
        _floorRepository = floorRepository;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        var floor = await _floorRepository.GetFloor(id);
        FloorDto = floor!.ToFloorItemDto();
        if (FloorDto is null)
        {
            return NotFound("floor not found");
        }

        return Page();
    }
}