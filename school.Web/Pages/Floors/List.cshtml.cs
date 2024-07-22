using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class FloorsList : BaseListPageModel
{
    private readonly IFloorRepository _floorRepository;

    public IEnumerable<FloorItemDto> FloorsDto { get; private set; } = default!;

    public FloorsList(ISchoolRepository schoolRepository, IFloorRepository floorRepository)
        : base(schoolRepository)
    {
        _floorRepository = floorRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        if (!await HasSelectedSchoolAsync())
        {
            return RedirectToSchoolList();
        }

        var floors = await _floorRepository.GetFloorsAsync(SelectedSchoolId);
        FloorsDto = floors.Select(f => f.ToFloorItemDto()).ToArray();

        return Page();
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var floor = await _floorRepository!.GetByIdAsync(id);

        if (floor is null)
        {
            return RedirectToPage("List");
        }

        await _floorRepository.DeleteAsync(floor!);
        return RedirectToPage("List");
    }
}