using SchoolManagement.Models;

namespace SchoolManagement.API.Features.Rooms.Dtos;

public record RoomCreateDto(int Number, RoomType Types, int FloorId);
