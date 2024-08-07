using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class AddFloor : BasePageModel
{
    private readonly IRepository<Floor> _floorRepository;

    public int FloorNumber { get; private set; }
    public string InValidMessage { get; private set; } = "";
    public bool IsError { get; private set; } = false;

    public AddFloor(ISchoolRepository schoolRepository, IRepository<Floor> floorRepository)
        :base(schoolRepository)
    {
        _floorRepository = floorRepository;
    }

    public async Task<IActionResult> OnGetAsync(bool? isError = false)
    {
        if (!await HasSelectedSchoolAsync())
        {
            return RedirectToSchoolList();
        }

        if(isError is true)
        {
            IsError = true;
        }

        var floors = await _floorRepository.GetAllAsync(f => f.SchoolId == SelectedSchoolId);
        if (!floors.Any())
        {
            FloorNumber = 1;
        }
        else
        {
            FloorNumber = floors.Last().Number + 1;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int floorNumber, bool isBasement)
    {
        if (floorNumber <= 0)
        {
            InValidMessage = "'Number' must be greater than '0'";
            return Page();
        }

        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var school = await SchoolRepository.GetByIdAsync(SelectedSchoolId);
        if (school is null)
        {
            return RedirectToSchoolList();
        }

        if (isBasement == true)
        {
            floorNumber = -floorNumber;
        }

        var floors = await _floorRepository.GetAllAsync(f => f.SchoolId == SelectedSchoolId);

        if(floors.Any(f => f.Number == floorNumber))
        {
            ErrorMessage = "Such floor already exists";
            return Page();
        }

        Floor floor = new(floorNumber)
        {
            School = school
        };

        await _floorRepository.AddAsync(floor);
        return RedirectToPage("List");
    }
}
