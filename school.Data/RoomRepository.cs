using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Data;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    private readonly AppDbContext _dbContext;
    public RoomRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Room>> GetRoomsWithFloorsAsync(Expression<Func<Room, bool>>? predicate = null,
    Func<IQueryable<Room>, IOrderedQueryable<Room>> orderBy = null!, int schoolId = 0)
    {
        var rooms = _dbContext.Rooms
            .Where(r => r.Floor.SchoolId == schoolId)
            .Include(r => r.Floor)
            .Where(predicate!);

        if (orderBy is not null)
        {
            rooms = orderBy(rooms);
        }

        return await rooms.ToArrayAsync();
    }

    public async Task<IEnumerable<Room>> GetRoomsForSchoolAsync(int schoolId)
    {
        var rooms = await _dbContext
            .Rooms
            .Where(r => r.Floor.SchoolId == schoolId)
            .ToArrayAsync();

        return rooms;
    }

    public async Task<IEnumerable<Room>> GetRoomsAsync(int schoolId)
    {
        var rooms = await _dbContext
            .Rooms
            .Where(r => r.Floor.SchoolId == schoolId)
            .Include(r => r.Floor)
            .ToArrayAsync();

        return rooms;
    }

    public async Task<Room> GetRoomAsync(int id)
    {
        var room = await _dbContext
            .Rooms
            .Where(r => r.Id == id)
            .Include(r => r.Floor)
            .SingleOrDefaultAsync();

        return room!;
    }

    public async Task<Room> GetSchoolRoomAsync(int schoolId, int roomId)
    {
        var room = await _dbContext
            .Rooms
            .Where(r => r.Floor.SchoolId == schoolId
                && r.Id == roomId)
            .SingleOrDefaultAsync();

        return room!;
    }

    public IQueryable<Room> GetRoomsQuery(
        int schoolId,
        string? searchTerm,
        string? sortColumn,
        string? sortOrder)
    {
        var roomsQuery = _dbContext.Rooms
            .Where(r => r.Floor.SchoolId == schoolId)
            .Include(r => r.Floor)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            roomsQuery = roomsQuery
                .Where(r => r.Number.ToString() == searchTerm);
        }

        var keySelector = GetSortProperty(searchTerm);

        roomsQuery = sortOrder?.ToLower() == "desc"
            ? roomsQuery.OrderByDescending(keySelector)
            : roomsQuery.OrderBy(keySelector);

        return roomsQuery;
    }

    private static Expression<Func<Room, object>> GetSortProperty(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "type" => room => room.Type,
            _ => room => room.Number
        };
    }
}
