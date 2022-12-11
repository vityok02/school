using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolFormModel : PageModel
{
    private readonly IRepository<School> _schoolRepository;

    public SchoolFormModel(IRepository<School> schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }
    public void OnGet()
    {

    }
    public IActionResult OnPost(string name, Address address, DateTime openingDate)
    {
        School school = new(name, address, openingDate);
        _schoolRepository.Add(school);
        return RedirectToPage("List");
    }
}
