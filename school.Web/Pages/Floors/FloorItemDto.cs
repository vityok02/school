using SchoolManagement.Web.Pages.Rooms;

namespace SchoolManagement.Web.Pages.Floors;

public record FloorItemDto(int Id, int Number, Dictionary<int, string> rooms);
