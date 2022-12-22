using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms
{
    public class ListModel : PageModel
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<Floor> _floorRepository;
        public IEnumerable<Room>? Rooms { get; private set; }
        public IEnumerable<Floor>? Floors { get; set; }
        public ListModel(IRepository<Room> roomRepository, IRepository<Floor> floorRepository)
        {
            _roomRepository = roomRepository;
            _floorRepository = floorRepository;
        }
        public IActionResult OnGet()
        {
            var sId = HttpContext.Request.Cookies["SchoolId"];
            if (!int.TryParse(sId, out int schoolId) || sId is null)
            {
                return NotFound("Room not found");
            }

            Rooms = _roomRepository.GetAll(r => r.Floor.SchoolId == schoolId);
            return Page();
        }
        public IActionResult OnPostDelete(int id)
        {
            var room = _roomRepository.Get(id);
            if (room is null)
            {
                return NotFound("Room not found");
            }

            _roomRepository.Delete(room);
            return RedirectToPage("List");
        }
    }
}
