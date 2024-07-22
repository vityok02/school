using FluentValidation;
using SchoolManagement.API.Features.Students.Dtos;

namespace SchoolManagement.API.Features.Students;

public class StudentValidator
    : AbstractValidator<IStudentDto>
{
    public StudentValidator()
    {
        RuleFor(s => s.FirstName).Length(1, 50);
        RuleFor(s => s.LastName).Length(1, 50);
        RuleFor(s => s.Age).InclusiveBetween(5, 90);
        RuleFor(s => s.Group).Length(1, 50);
    }
}
