using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Data;

public class PositionRepository : Repository<Position>, IPositionRepository
{
    private readonly AppDbContext _dbContext;

    public PositionRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Position>> GetSchoolPositionsAsync(int schoolId)
    {
        return await _dbContext
            .Positions
            .Where(p => p.Schools.Any(s => s.Id == schoolId))
            .ToArrayAsync(); ;
    }

    public async Task<IEnumerable<Position>> GetUnSelectedPositionsAsync(int schoolId)
    {
        return await _dbContext
            .Positions
            .Where(p => p.Schools.All(s => s.Id != schoolId))
            .ToArrayAsync();
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
