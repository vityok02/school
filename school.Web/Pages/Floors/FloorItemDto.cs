namespace SchoolManagement.Web.Pages.Floors;

public record FloorItemDto(int Id, int Number, IEnumerable<FloorRoomDto> Rooms);
