using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using SchoolManagement.Data;

namespace SchoolManagement.Web.Pages.Schools
{
    public class EditModel : PageModel
    {
        AppDbContext _dbCtx;
        private readonly IRepository<School> _schoolRepository;
        [BindProperty]
        public School School { get; set; }
        public EditModel(IRepository<School> schoolRepository, AppDbContext ctx)
        {
            _schoolRepository = schoolRepository;
            _dbCtx = ctx;
        }
        public IActionResult OnGet(int id)
        {
            School = _dbCtx.Schools.Find(id);
            return Page();
        }
        public IActionResult OnPost()
        {
            _dbCtx.Schools.Update(School);
            _dbCtx.SaveChanges();
            return RedirectToPage("List");
        }
    }
}
