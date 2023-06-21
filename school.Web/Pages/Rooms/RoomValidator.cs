using FluentValidation;

namespace SchoolManagement.Web.Pages.Rooms;

public class RoomValidator : AbstractValidator<IRoomDto>
{
    public RoomValidator()
    {
        RuleFor(r => r.Number).GreaterThan(0);
    }
}
