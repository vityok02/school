using FluentValidation;
using SchoolManagement.API.Positions.Dtos;

namespace SchoolManagement.API.Positions;

public class PositionValidator
    : AbstractValidator<IPositionDto>
{
    public PositionValidator() => RuleFor(p => p.Name).Length(3, 100);
}
