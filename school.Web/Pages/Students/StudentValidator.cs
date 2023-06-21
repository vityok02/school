using FluentValidation;

namespace SchoolManagement.Web.Pages.Students;

public class StudentValidator : AbstractValidator<IStudentDto>
{
    public StudentValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty().Length(1, 50);
        RuleFor(e => e.LastName).NotEmpty().Length(1, 50);
        RuleFor(e => e.Age).NotEmpty().InclusiveBetween(5, 18);
        RuleFor(e => e.Group).NotEmpty().Length(1, 20);
    }
}
