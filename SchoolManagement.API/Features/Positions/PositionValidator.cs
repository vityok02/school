using FluentValidation;
using SchoolManagement.API.Features.Positions.Dtos;

namespace SchoolManagement.API.Features.Positions;

public class PositionValidator
    : AbstractValidator<IPositionDto>
{
    public PositionValidator() => RuleFor(p => p.Name).Length(3, 100);
}
