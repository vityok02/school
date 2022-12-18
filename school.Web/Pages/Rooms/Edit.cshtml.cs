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
        public IEnumerable<Floor>? Floors { get; set; }
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
            var schoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]!);
            Floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);
            return Page();
        }
        public IActionResult OnPost(Room room, RoomType[] roomTypes)
        {
            //var schoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]!);
            var floor = _floorRepository.Get(room.Floor.Id);
            if (floor is null)
            {
                return NotFound("Floor not found");
            }
            Room.Floor = floor;

            RoomType roomType = 0;
            foreach (var rt in roomTypes) 
            {
                roomType |= rt;
            }
            Room.Type = roomType;

            _roomRepository.Update(Room!);
            return RedirectToPage("List");
        }
    }
}
