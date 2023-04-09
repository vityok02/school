using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools
{
    public class EditModel : BasePageModel
    {
        public SchoolDto SchooDto { get; private set; } = default!;

        public EditModel(ISchoolRepository schoolRepository)
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

            SchooDto = school.ToSchoolDto();

            return Page();
        }

        public IActionResult OnPost(int id, SchoolDto schoolDto)
        {
            var school = SchoolRepository.GetSchool(id);
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

            SchoolRepository.Update(school);
            return Redirect($"/schools/{id}");
        }
    }
}
