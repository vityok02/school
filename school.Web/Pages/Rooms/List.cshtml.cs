using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class ListModel : BasePageModel
{
    private readonly IRepository<Room> _roomRepository;

    public IEnumerable<Room>? Rooms { get; private set; }
    public IEnumerable<Floor>? Floors { get; private set; }

    public ListModel(IRepository<Room> roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public IActionResult OnGet()
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
            return RedirectToSchoolList();

        Rooms = _roomRepository.GetAll(r => r.Floor.SchoolId == schoolId);
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
