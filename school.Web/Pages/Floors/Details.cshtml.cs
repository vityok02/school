using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class DetailsModel : BasePageModel
{
    private readonly IFloorRepository _floorRepository;

    public FloorItemDto? FloorDto { get; private set; }

    public DetailsModel(ISchoolRepository schoolRepository, IRepository<Room> roomRepository, IFloorRepository floorRepository)
        :base(schoolRepository)
    {
        _floorRepository = floorRepository;
    }

    public IActionResult OnGet(int id)
    {
        var floor = _floorRepository.GetFloor(id);
        FloorDto = floor!.ToFloorItemDto();
        if (FloorDto is null)
        {
            return NotFound("floor not found");
        }

        return Page();
    }
}