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

    public IEnumerable<Employee> GetEmployees()
    {
        var employees = _dbContext
            .Employees
            .Include(e => e.Positions);

        return employees;
    }

    public IEnumerable<Employee> GetEmployees(Expression<Func<Employee, bool>> predicate,
    Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null!)
    {
        var employees = _dbContext
            .Employees
            .Include(e => e.Positions)
            .Where(predicate);

        if (orderBy is not null)
        {
            employees = orderBy(employees);
        }

        return employees.ToArray();
    }
    //public IEnumerable<Employee> GetSchoolEmployees(int schoolId, string filterByName = null!, int filterByAge = 0, string filterByJob = null!)
    //{
    //    bool filters<T>(T e) where T : Employee
    //    {
    //        return (string.IsNullOrEmpty(filterByName) || e.FirstName.Contains(filterByName))
    //            && (string.IsNullOrEmpty(filterByJob) || e.Job.Contains(filterByJob))
    //            && (filterByAge == 0 || e.Age == filterByAge);
    //    }

    //    var directors = _dbContext.Set<Director>()
    //        .Where(d => d.SchoolId == schoolId)
    //        .Where(filters)
    //        .ToArray();

    //    var teachers = _dbContext.Set<Teacher>()
    //        .Where(d => d.SchoolId == schoolId)
    //        .Where(filters)
    //        .ToArray();

    //    var employees = new List<Employee>();
    //    employees.AddRange(directors);
    //    employees.AddRange(teachers);

    //    return employees;
    //}
}
