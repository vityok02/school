using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Data.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Employee>> GetSchoolEmployeesWithPositionsAsync(
        Expression<Func<Employee, bool>> predicate,
        Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null!,
        int schoolId = 0)
    {
        var employees = _dbContext.Employees
            .Where(e => e.SchoolId == schoolId)
            .Include(e => e.Positions)
            .Where(predicate);

        if (orderBy is not null)
        {
            employees = orderBy(employees);
        }

        return await employees.ToArrayAsync();
    }

    public new async Task<Employee?> GetByIdAsync(int id)
    {
        var employee = await _dbContext.Employees
            .Where(e => e.Id == id)
            .Include(e => e.Positions)
            .SingleOrDefaultAsync()!;

        return employee!;
    }

    public async Task<Employee?> GetSchoolEmployee(int schoolId, int employeeId)
    {
        var employee = await _dbContext.Employees
            .Where(e => e.Id == employeeId && e.SchoolId == schoolId)
            .Include(e => e.Positions)
            .SingleOrDefaultAsync();

        return employee;
    }

    public IQueryable<Employee> GetEmployeesQuery(
        int schoolId,
        string? searchTerm,
        string? sortColumn,
        string? sortOrder)
    {
        var employeesQuery = _dbContext.Employees
            .Where(e => e.SchoolId == schoolId)
            .Include(e => e.Positions)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            employeesQuery = employeesQuery.Where(e =>
                e.FirstName.Contains(searchTerm) ||
                e.LastName.Contains(searchTerm) ||
                e.Age.ToString().Contains(searchTerm) ||
                e.Positions.Any(p => p.Name.Contains(searchTerm)));
        }

        var keySelector = GetSortProperty(sortColumn);

        employeesQuery = sortOrder?.ToLower() == "desc"
            ? employeesQuery.OrderByDescending(keySelector)
            : employeesQuery.OrderBy(keySelector);

        return employeesQuery;
    }

    private static Expression<Func<Employee, object>> GetSortProperty(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "first-name" => e => e.FirstName,
            "last-name" => e => e.LastName,
            "age" => e => e.Age,
            _ => e => e.Id
        };
    }
}
