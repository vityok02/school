using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Positions;

public static class PositionExtensions
{
    public static PositionDto ToPositionDto(this Position position)
    {
        return new PositionDto(
            position.Id, 
            position.Name);
    }
}
