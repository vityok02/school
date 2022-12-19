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

        public void OnGet(int id)
        {
            var schoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]!);
            Room = _roomRepository.Get(id);
            Floor = _floorRepository.GetAll().Where(f => f.Number == Room.Floor.Number && f.SchoolId == schoolId).SingleOrDefault();
        }
    }
}
