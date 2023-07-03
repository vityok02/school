using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Rooms;

public class ListModel : BaseRoomPageModel
{
    public IEnumerable<RoomItemDto> RoomDtos { get; private set; } = null!;
    public int FloorNumber { get; private set; }
    public string RoomNumberSort { get; set; } = null!;
    public string RoomTypeSort { get; set; } = null!;
    public string FloorNumberSort { get; set; } = null!;
    public int FilterByRoomNumber { get; set; }
    public RoomType FilterByRoomType { get; set; }
    public int FilterByFloorNumber { get; set; }
    public Dictionary<string, string> RoomNumberParams { get; private set; } = null!;
    public Dictionary<string, string> RoomTypeParams { get; private set; } = null!;
    public Dictionary<string, string> FloorNumberParams { get; private set; } = null!;

    public ListModel(ISchoolRepository schoolRepository, IFloorRepository floorRepository, IRoomRepository roomRepository)
        : base(schoolRepository, floorRepository, roomRepository)
    {
    }

    public async Task<IActionResult> OnGetAsync(string orderBy, int filterByRoomNumber, RoomType[] filterByRoomType, int filterByFloorNumber)
    {
        if (!HasSelectedSchool())
        {
            return RedirectToSchoolList();
        }

        if (!await HasFloor())
        {
            return RedirectToFloorList();
        }

        OrderBy = orderBy;
        RoomNumberSort = String.IsNullOrEmpty(orderBy) ? "roomNumber_desc" : "";
        RoomTypeSort = orderBy == "roomType" ? "roomType_desc" : "roomType";
        FloorNumberSort = orderBy == "floorNumber" ? "floorNumber_desc" : "floorNumber";

        FilterByRoomNumber = filterByRoomNumber;
        FilterByFloorNumber = filterByFloorNumber;

        RoomType roomType = 0;
        foreach (var rt in filterByRoomType)
        {
            roomType |= rt;
        }

        FilterByRoomType = roomType;

        var rooms = await _roomRepository.GetRoomsWithFloorsAsync(FilterBy(FilterByRoomNumber, FilterByRoomType, FilterByFloorNumber), 
            Sort(orderBy), SelectedSchoolId);

        RoomDtos = rooms.Select(r => r.ToRoomItemDto()).ToArray();

        var filterParams = GetFilters();

        FilterParams = new Dictionary<string, string>(filterParams)
        {
            {nameof(orderBy), orderBy }
        };

        RoomNumberParams = new Dictionary<string, string>(filterParams)
        {
            {nameof(orderBy), RoomNumberSort }
        };

        RoomTypeParams = new Dictionary<string, string>(filterParams)
        {
            {nameof(orderBy), RoomTypeSort }
        };

        FloorNumberParams = new Dictionary<string, string>(filterParams)
        {
            {nameof(orderBy), FloorNumberSort }
        };

        return Page();

        static Expression<Func<Room, bool>> FilterBy(int filterByRoomNumber, RoomType filterByRoomTypes, int filterByFloorNumber)
        {
            return r => (filterByRoomNumber == 0 || r.Number.ToString().Contains(filterByRoomNumber.ToString()))
                && (filterByRoomTypes == 0 || r.Type.HasFlag(filterByRoomTypes))
                && (filterByFloorNumber == 0 || r.Floor.Number == filterByFloorNumber);
        }

        static Func<IQueryable<Room>, IOrderedQueryable<Room>> Sort(string orderBy)
        {
            return orderBy switch
            {
                "roomNumber_desc" => r => r.OrderByDescending(r => r.Number),
                "roomType" => r => r.OrderBy(r => r.Type),
                "roomType_desc" => r => r.OrderByDescending(r => r.Type),
                "floorNumber" => r => r.OrderBy(r => r.Floor.Number),
                "floorNumber_desc" => r => r.OrderByDescending(r => r.Floor.Number),
                _ => r => r.OrderBy(r => r.Number),
            };
        }
    }

    private IDictionary<string, string> GetFilters()
    {
        var filterParams = new Dictionary<string, string>();

        if(FilterByRoomNumber > 0)
        {
            filterParams.Add(nameof(FilterByRoomNumber), FilterByRoomNumber.ToString());
        }

        if (FilterByRoomType > 0)
        {
            filterParams.Add(nameof(FilterByRoomType), FilterByRoomType.ToString());
        }

        if(FilterByFloorNumber > 0)
        {
            filterParams.Add(nameof(FilterByFloorNumber), FilterByFloorNumber.ToString());
        }

        return filterParams;
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var room = await _roomRepository.GetAsync(id);
        if (room is null)
        {
            return RedirectToPage("List");
        }

        await _roomRepository.DeleteAsync(room);
        return RedirectToPage("List");
    }
}