﻿namespace SchoolManagement.Web.Pages.Rooms;

public record AddRoomDto(int Number, int FloorId)
    : IRoomDto;
