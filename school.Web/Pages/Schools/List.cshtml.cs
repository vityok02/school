using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolListModel : PageModel
{
    private readonly IRepository<School> _schoolRepository;

    public static IEnumerable<School>? Schools { get; private set; }
    public bool Valid { get; set; } = true;

    public SchoolListModel(IRepository<School> schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public void OnGet()
    {
        Schools = _schoolRepository.GetAll();
    }

    public void OnGetError()
    {
        OnGet();
        Valid = false;
    }

    public IActionResult OnPostDelete(int id)
    {
        var school = _schoolRepository.Get(id);
        if (school is null)
        {
            return RedirectToPage("List");
        }

        _schoolRepository.Delete(school);
        return RedirectToPage("List");
    }
}
