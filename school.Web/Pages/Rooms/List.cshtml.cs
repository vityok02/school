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
        public ListModel(IRepository<Room> roomRepository, IRepository<Floor> floorRepository)
        {
            _roomRepository = roomRepository;
            _floorRepository = floorRepository;
        }
        public static IEnumerable<Room>? Rooms { get; private set; }
        public int? SchoolId { get; set; }
        public IEnumerable<Floor>? Floors { get; set; }
        public void OnGet()
        {
            SchoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]!);
            Floors = _floorRepository.GetAll(f => f.SchoolId == SchoolId);
            Rooms = _roomRepository.GetAll(r => r.Floor.SchoolId == SchoolId);
        }
        public IActionResult OnPostDelete(int id)
        {
            var room = _roomRepository.Get(id);
            _roomRepository.Delete(room);
            return RedirectToPage("List");
        }
    }
}
