using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class DetailsModel : BasePageModel
{
    private readonly IRepository<Room> _roomRepository;
    private readonly IRepository<Floor> _floorRepository;

    public Floor? Floor { get; private set; }
    public IEnumerable<Room>? Rooms { get; private set; }

    public DetailsModel(IRepository<School> schoolRepository, IRepository<Room> roomRepository, IRepository<Floor> floorRepository)
        :base(schoolRepository)
    {
        _roomRepository = roomRepository;
        _floorRepository = floorRepository;
    }

    public IActionResult OnGet(int id)
    {
        Floor = _floorRepository.Get(id);
        if (Floor is null)
        {
            return NotFound("floor not found");
        }

        Rooms = _roomRepository.GetAll(r => r.Floor == Floor);
        return Page();
    }
}