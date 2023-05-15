using FluentValidation;

namespace SchoolManagement.Web.Pages.Employees;

public class EmployeeValidator
    : AbstractValidator<IEmployeeDto>
{
    public EmployeeValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty().Length(1, 10);
        RuleFor(e => e.LastName).NotEmpty().Length(1, 10);
        RuleFor(e => e.Age).NotEmpty().InclusiveBetween(18, 65);
    }
}
