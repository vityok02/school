using SchoolManagement.Models;

namespace SchoolManagement.API.Rooms.Dtos;

public record RoomCreateDto(int Number, RoomType Types, int FloorId);
