﻿using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Rooms;

public static class RoomExtensions
{
    public static RoomItemDto ToRoomItemDto(this Room room)
    {
        return new RoomItemDto(
            room.Id,
            room.Number,
            room.Type.ToString(),
            room.Floor.Number);
    }

    public static EditRoomDto ToEditRoomDto(this Room room)
    {
        return new EditRoomDto(
            room.Id,
            room.Number,
            room.FloorId);
    }

    public static FloorDto ToFloorDto(this Floor floor)
    {
        return new FloorDto(
            floor.Id,
            floor.Number);
    }
}
