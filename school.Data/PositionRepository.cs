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

    public IEnumerable<Position> GetSchoolPositions(int schoolId)
    {
        var positions = _dbContext
            .Positions
            .Where(p => p.Schools.Any(s => s.Id == schoolId))
            .ToArray();

        return positions;
    }

    public IEnumerable<Position> GetUnSelectedPositions(int schoolId)
    {
        var positions = _dbContext
            .Positions
            .Where(p => p.Schools.All(s => s.Id != schoolId))
            .ToArray();

        return positions;
    }

    public Position GetPosition(int positionId)
    {
        return _dbContext.Set<Position>()
            .Where(p => p.Id == positionId)
            .Include(p => p.Schools)
            .Include(p => p.Employees)
            .SingleOrDefault()!;
    }
}
