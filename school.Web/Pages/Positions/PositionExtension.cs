using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Employees;

public static class PositionExtension
{
    public static PositionDto ToPositionDto(this Position position)
    {
        return new PositionDto(
            position.Id, 
            position.Name);
    }
}
