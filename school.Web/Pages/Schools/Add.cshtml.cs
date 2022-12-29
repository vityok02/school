using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolFormModel : BasePageModel
{
    private readonly IRepository<School> _schoolRepository;

    public SchoolFormModel(IRepository<School> schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public IActionResult OnPost(string name, Address address, DateTime openingDate)
    {
        var schools = _schoolRepository.GetAll();
        if (schools.Any(s => s.Name == name))
        {
            ErrorMessage = "School with this name already exists";
            return Page();
        }

        School school = new(name, address, openingDate);

        _schoolRepository.Add(school);
        return RedirectToPage("List");
    }
}
