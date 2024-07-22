using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    new Task<Employee?> GetByIdAsync(int id);
    IQueryable<Employee> GetEmployeesQuery(
        int schoolId, string? searchTerm, string? sortColumn, string? sortOrder);
    Task<Employee?> GetSchoolEmployee(int schoolId, int employeeId);
    Task<IEnumerable<Employee>> GetSchoolEmployeesWithPositionsAsync(
        Expression<Func<Employee, bool>> predicate,
        Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null!,
        int schoolId = 0);
}
