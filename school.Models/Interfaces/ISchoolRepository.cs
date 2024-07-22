using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface ISchoolRepository : IRepository<School>
{
    Task<bool> IsSchoolExists(int schoolId);
    Task<School> GetSchoolAsync(int id);
    Task<IEnumerable<School>> GetSchools(Expression<Func<School, bool>> predicate = null!,
        Func<IQueryable<School>, IOrderedQueryable<School>> orderBy = null!);
    IQueryable<School> GetSchoolsQuery(string? searchTerm, string? sortColumn, string? sortOrder);
}
