using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Rooms;

public static class RoomHelper
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

    public static async Task<IEnumerable<FloorDto>> GetFloorsAsync(IFloorRepository floorRepository, int schoolId)
    {
        var floors = await floorRepository.GetSchoolFloorsAsync(schoolId);
        return floors.Select(f => f.ToFloorDto()).ToArray();
    }
}
