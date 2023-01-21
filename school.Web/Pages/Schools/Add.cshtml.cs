using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolFormModel : BasePageModel
{
    public string Name { get; set; } = "";
    public Address Address { get; set; } = new Address();
    public DateTime OpeningDate { get; set; }

    public SchoolFormModel(IRepository<School> schoolRepository)
        : base(schoolRepository)
    {
    }

    public IActionResult OnPost(string name, Address address, DateTime openingDate)
    {
        var schools = SchoolRepository.GetAll();
        if (schools.Any(s => s.Name == name))
        {
            ErrorMessage = "School with this name already exists";
            Name = name;
            Address = address;
            OpeningDate = openingDate;

            return Page();
        }

        School school = new(name, address, openingDate);

        SchoolRepository.Add(school);

        SetSchoolId(school.Id);

        return RedirectToPage("List");
    }
}
