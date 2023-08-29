using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Data;

public class SchoolRepository : Repository<School>, ISchoolRepository
{
    private readonly AppDbContext _dbContext;

    public SchoolRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<School> GetSchoolAsync(int id)
    {
        var school = await _dbContext
            .Schools
            .Where(s => s.Id == id)
            .Include(s => s.Address)
            .SingleOrDefaultAsync();
        return school!;
    }

    public async Task<IEnumerable<School>> GetSchools(Expression<Func<School, bool>> predicate = null!,
        Func<IQueryable<School>, IOrderedQueryable<School>> orderBy = null!)
    {
        IQueryable<School> schools = _dbContext
            .Schools
            .Include(s => s.Address);

        if (predicate is not null)
        {
            schools = schools.Where(predicate);
        }

        if (orderBy is not null)
        {
            schools = orderBy(schools);
        }

        return await schools.ToArrayAsync();
    }
}