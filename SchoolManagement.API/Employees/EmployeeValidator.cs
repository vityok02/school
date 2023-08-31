using FluentValidation;
using SchoolManagement.API.Employees.Dtos;

namespace SchoolManagement.API.Employees;

public class EmployeeValidator
    : AbstractValidator<IEmployeeDto>
{
    public EmployeeValidator()
    {
        RuleFor(e => e.FirstName).Length(1, 50);
        RuleFor(e => e.LastName).Length(1, 50);
        RuleFor(e => e.Age).InclusiveBetween(18, 100);
    }
}
