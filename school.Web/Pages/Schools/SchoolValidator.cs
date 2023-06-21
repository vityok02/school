using FluentValidation;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolValidator : AbstractValidator<ISchoolDto>
{
    public SchoolValidator()
    {
        RuleFor(s => s.Name).NotEmpty().Length(1, 200);
        RuleFor(s => s.Country).NotEmpty().Length(1, 100);
        RuleFor(s => s.City).NotEmpty().Length(1, 100);
        RuleFor(s => s.Street).NotEmpty().Length(1, 200);
        RuleFor(s => s.PostalCode).NotEmpty().NotEqual(0);
        RuleFor(s => s.OpeningDate).NotEmpty();
    }
}
