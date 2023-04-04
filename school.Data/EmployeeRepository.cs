using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Data;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Employee> GetSchoolEmployees(Expression<Func<Employee, bool>> predicate,
        Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null!, int schoolId = 0)
    {
        var employees = _dbContext
            .Employees
            .Where(e => e.SchoolId == schoolId)
            .Include(e => e.Positions)
            .Where(predicate);

        if (orderBy is not null)
        {
            employees = orderBy(employees);
        }

        return employees.ToArray();
    }

    public Employee GetEmployee(int id)
    {
        var employee = _dbContext
            .Employees
            .Where(e => e.Id == id)
            .Include(e => e.Positions)
            .SingleOrDefault();

        return employee!;
    }
}
