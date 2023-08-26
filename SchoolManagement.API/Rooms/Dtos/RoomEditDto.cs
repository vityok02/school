using SchoolManagement.Models;

namespace SchoolManagement.API.Rooms.Dtos;

public record RoomEditDto(int Id, int Number, RoomType Types, int FloorId);
