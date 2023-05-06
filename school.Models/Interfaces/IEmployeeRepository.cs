using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee> GetEmployeeAsync(int id);
    Task<IEnumerable<Employee>> GetSchoolEmployeesAsync(Expression<Func<Employee, bool>> predicate, Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null!, int schoolId = 0);
}
