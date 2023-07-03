using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace SchoolManagement.Data;

public class PositionRepository : Repository<Position>, IPositionRepository
{
    private readonly AppDbContext _dbContext;

    public PositionRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Position>> GetSchoolPositionsAsync(int schoolId, Expression<Func<Position, bool>>? predicate = null,
        Func<IQueryable<Position>, IOrderedQueryable<Position>>? orderBy = null)
    {
        IQueryable<Position> positions = _dbContext
            .Set<Position>()
            .Where(p => p.Schools.Any(s => s.Id == schoolId));

        if (predicate is not null)
        {
            positions = positions.Where(predicate);
        }

        if (orderBy is not null)
        {
            positions = orderBy(positions);
        }
        return await positions.ToArrayAsync();
    }

    public async Task<IEnumerable<Position>> GetAllPositions(int schoolId, Expression<Func<Position, bool>>? predicate = null,
        Func<IQueryable<Position>, IOrderedQueryable<Position>>? orderBy = null)
    {
        IQueryable<Position> positions = _dbContext
            .Set<Position>();

        if (predicate is not null)
        {
            positions = positions.Where(predicate);
        }

        if (orderBy is not null)
        {
            positions = orderBy(positions);
        }
        return await positions.ToArrayAsync();
    }

    public async Task<Position> GetPositionAsync(int positionId)
    {
        var position = await _dbContext
            .Set<Position>()
            .Where(p => p.Id == positionId)
            .Include(p => p.Schools)
            .Include(p => p.Employees)
            .SingleOrDefaultAsync();

        return position!;
    }

    public async Task<IEnumerable<Position>> GetEmployeePositions(int[] checkedPositionsId)
    {
        return await _dbContext
            .Positions
            .Where(s => checkedPositionsId
                .Contains(s.Id))
            .ToArrayAsync();
    }
}
