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

    public IEnumerable<Room> GetRooms(Expression<Func<Room, bool>> predicate,
    Func<IQueryable<Room>, IOrderedQueryable<Room>> orderBy = null!, int schoolId = 0)
    {
        var rooms = _dbContext.Rooms
            .Where(r => r.Floor.SchoolId == schoolId)
            .Include(r => r.Floor)
            .Where(predicate);

        if (orderBy is not null)
        {
            rooms = orderBy(rooms);
        }

        return rooms.ToArray();
    }
}
