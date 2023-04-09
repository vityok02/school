using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using SchoolManagement.Models;
using SchoolManagement.Models.Dto;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolFormModel : BasePageModel
{
    [BindProperty]
    public School School { get; set; } = default!;
    public string Name { get; set; } = "";
    public Address Address { get; set; } = new Address();
    public DateTime OpeningDate { get; set; }

    public SchoolFormModel(ISchoolRepository schoolRepository)
        : base(schoolRepository)
    {
    }

    public IActionResult OnPost()
    {
        var schools = SchoolRepository.GetAll();

        if (schools.Any(s => s.Name == School.Name))
        {
            ErrorMessage = "School with this name already exists";
            Name = School.Name;
            Address = School.Address;
            OpeningDate = School.OpeningDate;

            return Page();
        }

        var school = new School
        {
            Name = School.Name,
            Address = School.Address,
            OpeningDate = OpeningDate,
        };

        SchoolDto schoolDto = school.ToSchoolDto();

        //SchoolRepository.Add(schoolDto);

        return Page();
        //return RedirectToPage("List");
    }
}
