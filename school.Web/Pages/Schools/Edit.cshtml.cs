using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class EditSchoolModel : BasePageModel
{
    private readonly IValidator<ISchoolDto> _validator;

    public SchoolDto SchoolDto { get; private set; } = default!;

    public EditSchoolModel(
        ISchoolRepository schoolRepository, 
        IValidator<ISchoolDto> validator)
        : base(schoolRepository)
    {
        _validator = validator;
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

    public async Task<IActionResult> OnPostAsync(SchoolDto schoolDto)
    {
        var validationResult = await _validator.ValidateAsync(schoolDto);

        if(!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, nameof(SchoolDto));

            SchoolDto = schoolDto;

            return Page();
        }

        var school = await SchoolRepository.GetSchoolAsync(schoolDto.Id);

        if (school is null)
        {
            SchoolDto = schoolDto;

            return RedirectToPage("List");
        }

        if (Schools.Any(s => s.Name == schoolDto.Name
            && s.Id != school.Id))
        {
            SchoolDto = schoolDto;

            ErrorMessage = "Such school already exists";

            return Page();
        }

        school.Name = schoolDto.Name;
        school.Address.Country = schoolDto.Country;
        school.Address.City = schoolDto.City;
        school.Address.Street = schoolDto.Street;
        school.Address.PostalCode = schoolDto.PostalCode;
        school.OpeningDate = schoolDto.OpeningDate;

        await SchoolRepository.UpdateAsync(school);
        return RedirectToPage("Details", new { id = schoolDto.Id });
    }
}
