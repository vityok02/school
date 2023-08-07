using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class ListModel : BasePageModel
{
    private readonly IFloorRepository _floorRepository;

    public IEnumerable<FloorItemDto> FloorsDto { get; private set; } = default!;

    public ListModel(ISchoolRepository schoolRepository, IFloorRepository floorRepository)
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

        var floors = await _floorRepository.GetSchoolFloorsAsync(SelectedSchoolId);
        FloorsDto = floors.Select(f => f.ToFloorItemDto()).ToArray();

        return Page();
    }

    //public IActionResult OnPostAddFloor()
    //{
    //    var schoolId = GetSchoolId();
    //    if (schoolId == -1)
    //    {
    //        return RedirectToSchoolList();
    //    }

    //    FloorsDto = _floorRepository!.GetAll(f => f.SchoolId == schoolId);

    //    var school = SchoolRepository.Get(schoolId);
    //    int number;

    //    if (!FloorsDto!.Any() || FloorsDto!.Last().Number < 0)
    //    {
    //        number = 0;
    //    }
    //    else
    //    {
    //        number = FloorsDto!.Last().Number;
    //    }

    //    Floor floor = new(number + 1)
    //    {
    //        School = school!
    //    };

    //    _floorRepository!.Add(floor);
    //    return RedirectToPage("List");
    //}
    //public IActionResult OnPostAddBasement()
    //{
    //    var schoolId = GetSchoolId();
    //    if (schoolId == -1)
    //    {
    //        return RedirectToSchoolList();
    //    }

    //    Floors = _floorRepository!.GetAll(f => f.SchoolId == schoolId);

    //    var school = SchoolRepository.Get(schoolId);
    //    int number;

    //    if (!Floors!.Any() || Floors!.First().Number >= 0)
    //        number = 0;
    //    else
    //        number = Floors!.First().Number;

    //    Floor floor = new(number - 1);

    //    floor.School = school!;

    //    _floorRepository!.Add(floor);
    //    return RedirectToPage("List");
    //}

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var floor = await _floorRepository!.GetAsync(id);

        if (floor is null)
        {
            return RedirectToPage("List");
        }

        await _floorRepository.DeleteAsync(floor!);
        return RedirectToPage("List");
    }
}