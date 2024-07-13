namespace SchoolManagement.API.Features.Floors.Dtos;

public record FloorDto(
    int Id,
    int Number,
    FloorRoomDto[] Rooms);
