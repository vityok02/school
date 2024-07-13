namespace SchoolManagement.API.Features.Positions.Dtos;

public record PositionDto(int Id, string Name)
    : IPositionDto;
