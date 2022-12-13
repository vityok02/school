using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors
{
    public class ListModel : PageModel
    {
        AppDbContext _ctx;
        private readonly IRepository<Floor>? _floorRepository;
        private readonly IRepository<School> _schoolRepository;

        public ListModel(IRepository<Floor>? floorRepository, IRepository<School> schoolRepository, AppDbContext ctx)
        {
            _floorRepository = floorRepository;
            _schoolRepository = schoolRepository;
            _ctx = ctx;
            Floors = _ctx.Floors.Where(f=>f.SchoolId == SchoolId);
        }
        public School School { get; set; }
        public IEnumerable<School> Schools { get; set; }
        public IEnumerable<Floor> Floors { get; set; }

        public int SchoolId { get; set; }


        public void OnGet()
        {
            SchoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]);
            Floors = _floorRepository.GetAll(f => f.SchoolId == SchoolId);
        }

        public void OnPostAddFloor()
        {
            SchoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]);
            School = _schoolRepository.Get(SchoolId);
            int number;
            if (Floors is null)
            {
                number = 0;
            }
            else
                number = Floors.Last().Number;
            Floor floor = new(number + 1);
            var (valid, error) = School.AddFloor(floor);
            _ctx.SaveChanges();
        }
    }
}
