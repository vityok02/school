using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class AddSchoolModel : BasePageModel
{
    public AddSchoolDto SchoolDto { get; private set; } = null!;
    private readonly IValidator<ISchoolDto> _validator;

    public AddSchoolModel(
        ISchoolRepository schoolRepository, 
        IValidator<ISchoolDto> validator)
        : base(schoolRepository)
    {
        _validator = validator;
    }

    public async Task<IActionResult> OnPostAsync(AddSchoolDto schoolDto)
    {
        var validationResult = await _validator.ValidateAsync(schoolDto);

        if (!validationResult.IsValid) 
        {
            validationResult.AddToModelState(ModelState, nameof(SchoolDto));

            return Page();
        }

        var schools = await SchoolRepository.GetAllAsync();

        if (schools.Any(s => s.Name == schoolDto.Name))
        {
            ViewData["ErrorMessage"] = "School with this name already exists";

            return Page();
        }

        Address address = new()
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
            OpeningDate = schoolDto.OpeningDate
        };

        await SchoolRepository.AddAsync(school);

        return RedirectToPage("List");
    }
}
