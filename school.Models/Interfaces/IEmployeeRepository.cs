using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Employee GetEmployee(int id);
    IEnumerable<Employee> GetSchoolEmployees(Expression<Func<Employee, bool>> predicate, Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null!, int schoolId = 0);
}
