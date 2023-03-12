using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    IEnumerable<Employee> GetEmployees();
    IEnumerable<Employee> GetEmployees(Expression<Func<Employee, bool>> predicate, Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null);
    //IEnumerable<Employee> GetSchoolEmployees(int schoolId, string filterByName, int filterByAge, string filterByJob);
}
