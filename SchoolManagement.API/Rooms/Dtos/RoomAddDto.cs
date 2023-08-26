using SchoolManagement.Models;

namespace SchoolManagement.API.Rooms.Dtos;

public record RoomAddDto(int Number, RoomType Types, int FloorId);
