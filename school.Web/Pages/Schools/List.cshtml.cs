using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolListModel : PageModel
{
    private readonly IRepository<School> _schoolRepository;
    private readonly IRepository<Address> _addressRepository;

    public static IEnumerable<School>? Schools { get; private set; }
    public IEnumerable<Address> Addresses { get; set; }
    public bool IsError { get; set; } = false;
    public bool IsFirst { get; set; } = true;

    public SchoolListModel(IRepository<School> schoolRepository, IRepository<Address> addressRepository)
    {
        _schoolRepository = schoolRepository;
        _addressRepository = addressRepository;
    }

    public void OnGet()
    {
        Schools = _schoolRepository.GetAll();
        Addresses = _addressRepository.GetAll();

        IsFirst = false;
    }

    public void OnGetFirstTime()
    {
        Schools = _schoolRepository.GetAll();
    }

    public void OnGetError()
    {
        OnGet();
        IsError = true;
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
