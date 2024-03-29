using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolDetailsModel : BasePageModel
{
    public SchoolDto SchoolDto { get; private set; } = default!;

    public SchoolDetailsModel(ISchoolRepository schoolRepository)
        : base(schoolRepository)
    {
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var school = await SchoolRepository.GetSchoolAsync(id);
        if (school is null)
        {
            return RedirectToPage("List");
        }

        SchoolDto = school.ToSchoolDto();

        return Page();
    }
}
