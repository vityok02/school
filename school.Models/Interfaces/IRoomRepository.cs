using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    IEnumerable<Room> GetRooms(Expression<Func<Room, bool>> predicate,
    Func<IQueryable<Room>, IOrderedQueryable<Room>> orderBy = null!, int schoolId = 0);
}
