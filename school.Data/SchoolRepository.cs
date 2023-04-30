using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Data;

public class SchoolRepository : Repository<School>, ISchoolRepository
{
    private readonly AppDbContext _dbContext;

    public SchoolRepository(AppDbContext dbContext)
        :base(dbContext)
    {
        _dbContext = dbContext;
    }

    public School GetSchool(int id)
    {
        return _dbContext.Schools.Where(s => s.Id == id).Include(s => s.Address).SingleOrDefault()!;
    }

    public IEnumerable<School> GetSchools(Expression<Func<School, bool>> predicate,
        Func<IQueryable<School>, IOrderedQueryable<School>> orderBy = null!)
    {
        var schools = _dbContext
            .Schools
            .Include(s => s.Address)
            .Where(predicate);

        if (orderBy is not null)
        {
            schools = orderBy(schools);
        }

        return schools;
    }
}