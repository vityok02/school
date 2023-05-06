using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Data;

public class FloorRepository : Repository<Floor>, IFloorRepository
{
    private readonly AppDbContext _dbContext;

    public FloorRepository(AppDbContext dbContext)
        : base (dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Floor> GetFloorAsync(int id)
    {
        var floor = await _dbContext
            .Floors
            .Where(f => f.Id == id)
            .Include(f => f.Rooms)
            .SingleOrDefaultAsync();

        return floor!;
    }

    public async Task<IEnumerable<Floor>> GetSchoolFloorsAsync(int schoolId)
    {
        return await _dbContext
            .Floors
            .Where(f => f.SchoolId == schoolId)
            .Include(f => f.Rooms).ToArrayAsync();
    }
}
