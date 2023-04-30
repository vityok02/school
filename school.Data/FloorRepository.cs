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

    public Floor? GetFloor(int id)
    {
        return _dbContext
            .Floors
            .Where(f => f.Id == id)
            .Include(f => f.Rooms)
            .SingleOrDefault();
    }

    public IEnumerable<Floor> GetFloors(int schoolId)
    {
        return _dbContext
            .Floors
            .Where(f => f.SchoolId == schoolId)
            .Include(f => f.Rooms).ToArray();
    }
}
