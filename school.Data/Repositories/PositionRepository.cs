using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Data.Repositories;

public class PositionRepository : Repository<Position>, IPositionRepository
{
    private readonly AppDbContext _dbContext;

    public PositionRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Position>> GetSchoolPositions(int schoolId, Expression<Func<Position, bool>>? predicate = null,
        Func<IQueryable<Position>, IOrderedQueryable<Position>>? orderBy = null)
    {
        IQueryable<Position> positions = _dbContext
            .Positions
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
            .Positions;

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

    public async Task<Position> GetPosition(int positionId)
    {
        var position = await _dbContext
            .Positions
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
            .Where(s => checkedPositionsId.Contains(s.Id))
            .ToArrayAsync();
    }

    public async Task<bool> HasSchoolPositions(int schoolId)
    {
        return await _dbContext
            .Set<Position>()
            .AnyAsync(p => p.Schools.Any(s => s.Id == schoolId));
    }

    public async Task<Position> GetSchoolPosition(int schoolId, int positionId)
    {
        var position = await _dbContext
            .Positions
            .Where(p =>
                p.Id == positionId &&
                p.Schools.Any(s => s.Id == schoolId))
            .SingleOrDefaultAsync();

        return position!;
    }

    public IQueryable<Position> GetAllPositionsQueryable(
        string? searchTerm,
        string? sortOrder,
        int? schoolId = null)
    {
        var positionsQueryable = _dbContext.Positions
            .AsQueryable();

        if (schoolId is not null)
        {
            positionsQueryable = positionsQueryable
                .Where(p => p.Schools.Any(s => s.Id == schoolId));
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            positionsQueryable = positionsQueryable
                .Where(p => p.Name.Contains(searchTerm));
        }

        positionsQueryable = sortOrder?.ToLower() == "desc"
            ? positionsQueryable.OrderByDescending(p => p.Name)
            : positionsQueryable.OrderBy(p => p.Name);

        return positionsQueryable;
    }
}
