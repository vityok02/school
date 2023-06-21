namespace SchoolManagement.Web.Pages.Rooms;

public record EditRoomDto(int Id, int Number, int FloorId)
    : IRoomDto;
