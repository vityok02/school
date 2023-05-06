using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface IPositionRepository : IRepository<Position>
{
    Task<IEnumerable<Position>> GetSchoolPositionsAsync(int schoolId, Expression<Func<Position, bool>>? predicate = null,
        Func<IQueryable<Position>, IOrderedQueryable<Position>>? orderBy = null);
    Task<IEnumerable<Position>> GetUnSelectedPositionsAsync(int schoolId, Expression<Func<Position, bool>>? predicate = null,
        Func<IQueryable<Position>, IOrderedQueryable<Position>>? orderBy = null);
    Task<Position> GetPositionAsync(int positionId);
    Task<IEnumerable<Position>> GetEmployeePositions(int[] checkedPositionsId);
}
