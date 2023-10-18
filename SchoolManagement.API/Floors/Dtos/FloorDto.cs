namespace SchoolManagement.API.Floors.Dtos;

public record FloorDto(
    int Id,
    int Number,
    FloorRoomDto[] Rooms);
