namespace SchoolManagement.API.Positions.Dtos;

public record PositionCreateDto(string Name)
    : IPositionDto;
