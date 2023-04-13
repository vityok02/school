using SchoolManagement.Models;
using SchoolManagement.Web.Pages.Positions;

namespace SchoolManagement.Web.Pages.Employees;

public static class PositionExtensions
{
    public static PositionDto ToPositionDto(this Position position)
    {
        return new PositionDto(
            position.Id, 
            position.Name);
    }
}
