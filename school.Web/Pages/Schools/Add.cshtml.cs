using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolFormModel : PageModel
{
    private readonly IRepository<School> _schoolRepository;

    public string Message { get; private set; } = "";

    public SchoolFormModel(IRepository<School> schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public IActionResult OnPost(string name, Address address, DateTime openingDate)
    {
        var schools = _schoolRepository.GetAll();
        if (schools.Any(s => s.Name == name))
        {
            Message = "School with this name already exists";
            return Page();
        }

        School school = new(name, address, openingDate);

        _schoolRepository.Add(school);
        return RedirectToPage("List");
    }
}
