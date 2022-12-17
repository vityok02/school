using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms
{
    public class EditModel : PageModel
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<Floor> _floorRepository;
        [BindProperty]
        public Room? Room { get; set; }
        [BindProperty]
        public static IEnumerable<Floor>? Floors { get; set; }
        [BindProperty]
        public Floor? Floor { get; set; }
        public EditModel(IRepository<Room> roomRepository, IRepository<Floor> floorRepository)
        {
            _roomRepository = roomRepository;
            _floorRepository = floorRepository;
        }

        public IActionResult OnGet(int id)
        {
            Room = _roomRepository.Get(id);
            var schoolId = int.Parse(HttpContext.Request.Cookies["schoolId"]!);
            Floors = _floorRepository.GetAll().Where(f => f.SchoolId == schoolId);
            return Page();
        }
        public IActionResult OnPost()
        {
            _roomRepository.Update(Room!);
            return RedirectToPage("List");
        }
    }
}
