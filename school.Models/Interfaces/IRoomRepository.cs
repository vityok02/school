using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    Task<IEnumerable<Room>> GetRoomsWithFloorsAsync(Expression<Func<Room, bool>>? predicate = null,
    Func<IQueryable<Room>, IOrderedQueryable<Room>> orderBy = null!, int schoolId = 0);
    Task<IEnumerable<Room>> GetRoomsAsync(int schoolId);
    Task<Room> GetRoomAsync(int schoolId);
    Task<IEnumerable<Room>> GetRoomsForSchoolAsync(int schoolId);
}
