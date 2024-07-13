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

    public IQueryable<School> GetSchoolsQuery(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder)
    {
        IQueryable<School> schoolsQuery = _dbContext
            .Schools
            .Include(s => s.Address);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            schoolsQuery = schoolsQuery.Where(s =>
                s.Name.Contains(searchTerm) ||
                s.Address.Country.Contains(searchTerm) ||
                s.Address.City.Contains(searchTerm) ||
                s.Address.Street.Contains(searchTerm));
        }

        Expression<Func<School, object>> keySelector = GetSortProperty(sortColumn);

        if (sortOrder?.ToLower() == "desc")
        {
            schoolsQuery = schoolsQuery.OrderByDescending(keySelector);
        }
        else
        {
            schoolsQuery = schoolsQuery.OrderBy(keySelector);
        }

        return schoolsQuery;
    }

    public async Task<bool> DoesSchoolExist(int schoolId)
    {
        return await _dbContext.Schools.AnyAsync(s => s.Id == schoolId);
    }

    private static Expression<Func<School, object>> GetSortProperty(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "name" => school => school.Name,
            "country" => school => school.Address.Country,
            "city" => school => school.Address.City,
            "street" => school => school.Address.Street,
            _ => school => school.Id
        };
    }
}