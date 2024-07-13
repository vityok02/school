namespace SchoolManagement.API.Positions.Dtos;

public record PositionDto(int Id, string Name)
    : IPositionDto;
