using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors
{
    public class ListModel : PageModel
    {
        private readonly IRepository<Floor>? _floorRepository;
        private readonly IRepository<School> _schoolRepository;

        public ListModel(IRepository<Floor>? floorRepository, IRepository<School> schoolRepository)
        {
            _floorRepository = floorRepository;
            _schoolRepository = schoolRepository;
        }

        public IEnumerable<School> Schools { get; set; }
        public IEnumerable<Floor> Floors { get; set; }

        public int SchoolId { get; set; }


        public void OnGet()
        {
            SchoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]);
            Floors = _floorRepository.GetAll(f => f.SchoolId == SchoolId);
        }
    }
}
