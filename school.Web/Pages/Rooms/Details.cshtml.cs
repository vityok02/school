using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Web.Pages.Floors;

namespace SchoolManagement.Web.Pages.Rooms
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<Floor> _floorRepository;
        public Room? Room { get; set; }
        public Floor? Floor { get; set; }
        public DetailsModel(IRepository<Room> roomRepository, IRepository<Floor> floorRepository)
        {
            _roomRepository = roomRepository;
            _floorRepository = floorRepository;
        }

        public IActionResult OnGet(int id)
        {
            var sId = HttpContext.Request.Cookies["SchoolId"];
            if (!int.TryParse(sId, out int schoolId))
            {
                return NotFound("School not found");
            }

            Room = _roomRepository.Get(id);
            if (Room is null)
            {
                return NotFound("Room not found");
            }

            Floor = _floorRepository.GetAll().Where(f => f.Id == Room.Floor.Id).SingleOrDefault();
            return Page();
        }
    }
}
