using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using school.Data;
using school.Models;
using school.Models.Interfaces;

namespace school.Web.Pages
{
    public class SchoolModel : PageModel
    {
        private readonly IRepository<School> _schoolRepository;

        public IEnumerable<School> Schools { get; private set; }
        public SchoolModel(IRepository<School> schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public void OnGet()
        {
            Schools = _schoolRepository.GetAll();
        }
    }
}
