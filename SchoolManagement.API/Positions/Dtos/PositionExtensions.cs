using SchoolManagement.Models;

namespace SchoolManagement.API.Positions.Dtos;

public static class PositionExtensions
{
    public static PositionDto ToPositionDto(this Position position)
    {
        return new PositionDto(
            position.Id,
            position.Name);
    }
}
