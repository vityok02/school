using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolModel : BasePageModel
{
    public SchoolDto School { get; private set; } = default!;

    public SchoolModel(ISchoolRepository schoolRepository)
        :base(schoolRepository)
    {
    }

    public IActionResult OnGet(int id)
    {
        var school = SchoolRepository.GetSchool(id);

        if (school is null)
        {
            return RedirectToPage("List");
        }

        School = school.ToSchoolDto();

        SelectedSchoolName = School!.Name;

        return Page();
    }
}
