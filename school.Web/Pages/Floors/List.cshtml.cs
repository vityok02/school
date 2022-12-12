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
        public ICollection<Floor> Floors { get; set; }

        public int sId { get; set; }


        public void OnGet()
        {
            sId = int.Parse(HttpContext.Request.Cookies["SchoolId"]);
            Floors = (ICollection<Floor>)_floorRepository.GetAll(f => f.SchoolId == sId);
        }

        public void OnPostAddFloor()
        {
            sId = int.Parse(HttpContext.Request.Cookies["SchoolId"]);
            var school = _schoolRepository.GetAll().Where(s => s.Id == sId);
            int number;
            if (Floors is null)
            {
                number = 0;
            }
            else
                number = Floors.Last().Number;
            Floor floor = new(number + 1);
            Floors.Add(floor);
        }
    }
}
