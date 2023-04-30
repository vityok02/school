using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface ISchoolRepository : IRepository<School>
{
    public School GetSchool(int id);
    public IEnumerable<School> GetSchools(Expression<Func<School, bool>> predicate,
        Func<IQueryable<School>, IOrderedQueryable<School>> orderBy = null!);
}
