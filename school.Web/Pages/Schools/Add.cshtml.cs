using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class Add : BasePageModel
{
    public SchoolDto SchoolDto { get; private set; } = default!;

    public Add(ISchoolRepository schoolRepository)
        : base(schoolRepository)
    {
    }

    public IActionResult OnPost(SchoolDto schoolDto)
    {
        var schools = SchoolRepository.GetAll();

        if (schools.Any(s => s.Name == schoolDto.Name))
        {
            ErrorMessage = "SchoolDto with this name already exists";

            return Page();
        }

        var address = new Address()
        {
            Country = schoolDto.Country,
            City = schoolDto.City,
            Street = schoolDto.Street,
            PostalCode = schoolDto.PostalCode,
        };

        var school = new School()
        {
            Name = schoolDto.Name,
            Address = address,
            OpeningDate = schoolDto.OpeningDate.ToDateTime(TimeOnly.MinValue),
        };

        SchoolRepository.Add(school);

        return RedirectToPage("List");
    }
}
