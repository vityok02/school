namespace SchoolManagement.API.Features.Positions.Dtos;

public record PositionCreateDto(string Name)
    : IPositionDto;
