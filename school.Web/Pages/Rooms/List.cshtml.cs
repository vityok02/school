using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Rooms;

public class ListModel : BasePageModel
{
    private readonly IRepository<Room> _roomRepository;
    private readonly IRepository<Floor> _floorRepository;

    public IEnumerable<Room> Rooms { get; private set; } = null!;
    public IEnumerable<Floor> Floors { get; private set; } = null!;
    public string RoomNumberSort { get; set; } = null!;
    public string RoomTypeSort { get; set; } = null!;
    public string FloorNumberSort { get; set; } = null!;
    public int FilterByRoomNumber { get; set; }
    public RoomType FilterByRoomType { get; set; }
    public int FilterByFloorNumber { get; set; }
    public Dictionary<string, string> RoomNumberParams { get; private set; } = null!;
    public Dictionary<string, string> RoomTypeParams { get; private set; } = null!;
    public Dictionary<string, string> FloorNumberParams { get; private set; } = null!;

    public ListModel(IRepository<School> schoolRepository, IRepository<Room> roomRepository, IRepository<Floor> floorRepository)
        : base(schoolRepository)
    {
        _roomRepository = roomRepository;
        _floorRepository = floorRepository;
    }

    public IActionResult OnGet(string orderBy, int filterByRoomNumber, RoomType[] filterByRoomType, int filterByFloorNumber)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        OrderBy = orderBy;
        RoomNumberSort = orderBy == "roomNumber" ? "roomNumber_desc" : "roomNumber";
        RoomTypeSort = orderBy == "roomType" ? "roomType_desc" : "roomType";
        FloorNumberSort = orderBy == "floorNumber" ? "floorNumber_desc" : "floorNumber";

        FilterByRoomNumber = filterByRoomNumber;
        FilterByRoomType = RoomHelper.GetRoomType(filterByRoomType);
        FilterByFloorNumber = filterByFloorNumber;

        Rooms = _roomRepository.GetAll(FilterBy(FilterByRoomNumber, FilterByRoomType, FilterByFloorNumber, schoolId), 
            Sort(orderBy));

        Floors = _floorRepository.GetAll(f => f.SchoolId == schoolId);

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

        static Expression<Func<Room, bool>> FilterBy(int filterByRoomNumber, RoomType filterByRoomTypes, int filterByFloorNumber, int schoolId)
        {
            return r => r.Floor.SchoolId == schoolId
                && (filterByRoomNumber == 0 || r.Number.ToString().Contains(filterByRoomNumber.ToString()))
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

    public IActionResult OnPostDelete(int id)
    {
        var room = _roomRepository.Get(id);
        if (room is null)
        {
            return RedirectToPage("List");
        }

        _roomRepository.Delete(room);
        return RedirectToPage("List");
    }
}