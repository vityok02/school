using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages
{
    public class CurrentSchoolModel : PageModel
    {
        private readonly IRepository<School> _schoolRepository;
        public IEnumerable<School> Schools { get; private set; }
        public string Message { get; private set; } = "";
        public int Id { get; set; }
        public CurrentSchoolModel(IRepository<School> schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public void OnGet(int id)
        {
            Schools = _schoolRepository.GetAll();
            Id = id;
            var currentSchool = Schools.Where(s => s.Id == id).SingleOrDefault();
        }
    }
}
