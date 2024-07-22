using SchoolManagement.Models;

namespace SchoolManagement.API.Features.Rooms.Dtos;

public record RoomUpdateDto(int Id, int Number, RoomType Types, int FloorId);
