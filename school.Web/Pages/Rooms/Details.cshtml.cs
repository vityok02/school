using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public class DetailsModel : BasePageModel
{
    private readonly IRepository<Room> _roomRepository;
    private readonly IRepository<Floor> _floorRepository;

    public Room? Room { get; private set; }
    public Floor? Floor { get; private set; }

    public DetailsModel(IRepository<Room> roomRepository, IRepository<Floor> floorRepository)
    {
        _roomRepository = roomRepository;
        _floorRepository = floorRepository;
    }

    public IActionResult OnGet(int id)
    {
        Room = _roomRepository.Get(id);
        if (Room is null)
        {
            return RedirectToPage("List");
        }

        Floor = _floorRepository.GetAll().Where(f => f.Id == Room.Floor.Id).SingleOrDefault();
        return Page();
    }
}
