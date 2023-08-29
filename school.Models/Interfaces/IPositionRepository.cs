using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface IPositionRepository : IRepository<Position>
{
    Task<IEnumerable<Position>> GetSchoolPositions(int schoolId, Expression<Func<Position, bool>>? predicate = null,
        Func<IQueryable<Position>, IOrderedQueryable<Position>>? orderBy = null);
    Task<bool> HasSchoolPositions(int schoolId);
    Task<IEnumerable<Position>> GetAllPositions(int schoolId, Expression<Func<Position, bool>>? predicate = null,
        Func<IQueryable<Position>, IOrderedQueryable<Position>>? orderBy = null);
    Task<Position> GetPosition(int positionId);
    Task<IEnumerable<Position>> GetEmployeePositions(int[] checkedPositionsId);
    Task<Position> GetPositionForSchool(int positionId, int schoolId);
}
