using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using school.Data;
using school.Models;
using school.Models.Interfaces;

namespace school.Web.Pages
{
    public class RoomFormModel : PageModel
    {
        AppDbContext _dbContext;
        public readonly IRepository<School> _schoolRepository;
        public IEnumerable<School> Schools { get; private set; }
        public string Message { get; private set; } = "";
        public RoomFormModel(IRepository<School> schoolRepository, AppDbContext db)
        {
            _schoolRepository = schoolRepository;
            _dbContext = db;
        }
        public void OnGet()
        {
            Schools = _schoolRepository.GetAll();
        }
        public IActionResult OnPost(int id, int roomNumber, RoomType roomType, int floorNumber) 
        {
            var currentFloor = _dbContext.Floors
                .Where(f => f.SchoolId == id && f.Number == floorNumber)
                .Include(f => f.Rooms)
                .SingleOrDefault();

            if(currentFloor is null) 
            {
                Message = $"Floor {floorNumber} does not exists";
                return Page();
            }

            var (valid, error) = currentFloor.AddRoom(new Room(roomNumber, roomType));
            if (!valid)
            {
                Message = error!;
                return Page();
            }
            else
                return Redirect($"/school/{id}");
        }
    }
}
