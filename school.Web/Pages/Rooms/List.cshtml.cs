using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class ListModel : BasePageModel
{
    private readonly IRepository<Room> _roomRepository;
    private readonly IRepository<Floor> _floorRepository;

    public IEnumerable<Room> Rooms { get; private set; } = null!;
    public IEnumerable<Floor> Floors { get; private set; } = null!;

    public ListModel(IRepository<School> schoolRepository, IRepository<Room> roomRepository, IRepository<Floor> floorRepository)
        : base(schoolRepository)
    {
        _roomRepository = roomRepository;
        _floorRepository = floorRepository;
    }

    public IActionResult OnGet(string orderBy)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Rooms = _roomRepository.GetAll(r => r.Floor.SchoolId == schoolId).OrderBy(r => r.Number);
        Floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);

        return Page();
    }


    public IActionResult OnPostDelete(int id)
    {
        var room = _roomRepository.Get(id);
        if (room is null)
        {
            return RedirectToPage("List");
        }

        _roomRepository.Delete(room);
        return RedirectToPage("List");
    }
}
