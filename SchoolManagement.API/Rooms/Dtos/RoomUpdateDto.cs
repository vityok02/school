using SchoolManagement.Models;

namespace SchoolManagement.API.Rooms.Dtos;

public record RoomUpdateDto(int Id, int Number, RoomType Types, int FloorId);
