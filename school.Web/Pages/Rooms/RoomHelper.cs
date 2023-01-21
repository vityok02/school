using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Rooms
{
    public class RoomHelper
    {
        public static RoomType GetRoomType(RoomType[] roomTypes)
        {
            RoomType roomType = 0;
            foreach (var rt in roomTypes)
            {
                roomType |= rt;
            }

            return roomType;
        }
    }
}
