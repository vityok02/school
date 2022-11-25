using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using school.Data;
using school.Models;
using school.Models.Interfaces;

namespace school.Web.Pages
{
    public class FloorFormModel : PageModel
    {
        AppDbContext _dbContext;
        private readonly IRepository<School> _schoolRepository;
        public IEnumerable<School> Schools { get; private set; }
        public string Message { get; set; } = "";
        public FloorFormModel(IRepository<School> schoolRepository, AppDbContext db)
        {
            _schoolRepository = schoolRepository;
            _dbContext = db;
        }
        public void OnGet()
        {
            Schools = _schoolRepository.GetAll();
        }
        public IActionResult OnPost(int number, int id)
        {
            var currentSchool = _dbContext.Schools
                .Where(s => s.Id == id)
                .Include(s => s.Floors)
                .SingleOrDefault();
            Floor floor = new(number);
            var (valid, error) = currentSchool!.AddFloor(floor);
            if (!valid)
            {
                Message = error!;
                return Page();
            }
            
            _dbContext.SaveChanges();
            return Redirect($"/school/{id}");
        }
    }
}
