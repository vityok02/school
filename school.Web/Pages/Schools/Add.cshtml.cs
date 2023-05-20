using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class AddModel : BasePageModel
{
    public AddSchoolDto SchoolDto { get; private set; } = null!;
    private readonly IValidator<ISchoolDto> _validator;

    public AddModel(
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
            ErrorMessage = "School with this name already exists";

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
