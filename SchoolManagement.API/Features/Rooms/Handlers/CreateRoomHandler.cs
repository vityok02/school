using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Rooms.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Rooms.Handlers;

public static class CreateRoomHandler
{
    public static async Task<IResult> Handle(
        IRoomRepository roomRepository,
        IFloorRepository floorRepository,
        [FromRoute] int schoolId,
        [FromBody] RoomCreateDto roomDto)
    {
        if (await roomRepository.AnyAsync(r => r.Number == roomDto.Number))
        {
            return Results.Conflict(RoomErrorMessages.Dublicate);
        }

        var room = new Room()
        {
            Number = roomDto.Number,
            Type = roomDto.Types,
            FloorId = roomDto.FloorId,
        };

        var floor = await floorRepository.GetAsync(room.FloorId);

        if (floor is null)
        {
            return Results.NotFound(RoomErrorMessages.FloorNotFound);
        }

        room.Floor = floor;

        await roomRepository.AddAsync(room);

        var createdRoomDto = room.ToRoomDto();
        return Results.Created($"/schools/{schoolId}/rooms/{room.Id}", createdRoomDto);
    }
}
