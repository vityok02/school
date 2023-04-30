using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class EditModel : BasePageModel
{
    public SchoolDto SchoolDto { get; private set; } = default!;

    public EditModel(ISchoolRepository schoolRepository)
        : base(schoolRepository)
    {
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var school = await SchoolRepository.GetAsync(id);
        if (school is null)
        {
            return RedirectToPage("List");
        }

        SchoolDto = school.ToSchoolDto();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(SchoolDto schoolDto)
    {
        var school = await SchoolRepository.GetSchoolAsync(schoolDto.Id);
        if (school is null)
        {
            return RedirectToPage("List");
        }

        school.Name = schoolDto.Name;
        school.Address.Country = schoolDto.Country;
        school.Address.City = schoolDto.City;
        school.Address.Street = schoolDto.Street;
        school.Address.PostalCode = schoolDto.PostalCode;
        school.OpeningDate = schoolDto.OpeningDate.ToDateTime(TimeOnly.MinValue);

        await SchoolRepository.UpdateAsync(school);
        return RedirectToPage("Details", new { id = schoolDto.Id });
    }
}
