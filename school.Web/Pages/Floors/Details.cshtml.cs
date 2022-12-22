using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<Floor> _floorRepository;
        public Floor? Floor { get; set; }
        public IEnumerable<Room>? Rooms { get; set; }
        public DetailsModel(IRepository<Room> roomRepository, IRepository<Floor> floorRepository)
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
}
