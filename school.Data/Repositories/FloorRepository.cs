using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Data.Repositories;

public class FloorRepository : Repository<Floor>, IFloorRepository
{
    private readonly AppDbContext _dbContext;

    public FloorRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Floor> GetFloor(int id)
    {
        var floor = await _dbContext
            .Floors
            .Where(f => f.Id == id)
            .Include(f => f.Rooms)
            .SingleOrDefaultAsync();

        return floor!;
    }

    public async Task<Floor> GetSchoolFloorAsync(int schoolId, int floorId)
    {
        var floor = await _dbContext
            .Floors
            .Where(f => f.SchoolId == schoolId
                && f.Id == floorId)
            .Include(f => f.Rooms)
            .SingleOrDefaultAsync();

        return floor!;
    }

    public async Task<IEnumerable<Floor>> GetFloorsAsync(int schoolId)
    {
        return await _dbContext
            .Floors
            .Where(f => f.SchoolId == schoolId)
            .Include(f => f.Rooms).ToArrayAsync();
    }

    public IQueryable<Floor> GetFloorsQuery(
        int schoolId,
        string? sortOrder)
    {
        IQueryable<Floor> floorsQuery = _dbContext
            .Floors
            .Where(f => f.SchoolId == schoolId)
            .Include(f => f.Rooms);

        floorsQuery = sortOrder?.ToLower() == "desc"
            ? floorsQuery.OrderByDescending(f => f.Number)
            : floorsQuery.OrderBy(f => f.Number);

        return floorsQuery;
    }
}
