using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolListModel : PageModel
{
    AppDbContext _dbCtx;
    private readonly IRepository<School> _schoolRepository;
    public static IEnumerable<School> Schools { get; private set; }
    public SchoolListModel(IRepository<School> schoolRepository, AppDbContext ctx)
    {
        _schoolRepository = schoolRepository;
        _dbCtx = ctx;
    }

    public void OnGet()
    {
        Schools = _schoolRepository.GetAll();
        //Schools = _dbCtx.Schools.AsNoTracking().ToList();
    }

    public void OnPost()
    {

    }

    public IActionResult OnPostDelete(int id)
    {
        var school = _schoolRepository.GetAll()
            .Where(s => s.Id == id)
            .SingleOrDefault();
        //var school = _dbCtx.Schools.Find(id);

        if (school != null)
        {
            _schoolRepository.Delete(school);
        }
        return RedirectToPage("List");
    }
}
