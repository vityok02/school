using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetEmployeeWithPositionsAsync(int id);
    Task<Employee?> GetSchoolEmployeesWithPositionsAsync(int schoolId, int employeeId);
    Task<IEnumerable<Employee>> GetSchoolEmployeesWithPositionsAsync(Expression<Func<Employee, bool>> predicate, Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null!, int schoolId = 0);
}
