using FluentValidation;

namespace SchoolManagement.Web.Pages.Positions;

public class PositionValidator : AbstractValidator<PositionDto>
{
    public PositionValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(5);
    }
}
