﻿namespace SchoolManagement.Models;

public class Room : BaseEntity
{
    public int Number { get; set; }
    public RoomType Type { get; set; }
    public Floor Floor { get; set; } = null!;
    public int FloorId { get; set; }

    public Room()
    {
    }

    public Room(int number, RoomType type, Floor floor)
    {
        Number = number;
        Type = type;
        Floor = floor;
    }


    public override string ToString()
    {
        return $"Room: {Number}, {Type}";
    }
}
