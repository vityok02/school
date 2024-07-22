using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Data.Repositories;
public class StudentRepository : Repository<Student>, IStudentRepository
{
    private readonly AppDbContext _dbContext;

    public StudentRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Student> GetStudentsQuery(int schoolId, string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var studentsQuery = _dbContext.Students
            .Where(s => s.SchoolId == schoolId);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            studentsQuery = studentsQuery
                .Where(s => 
                    s.FirstName.Contains(searchTerm) ||
                    s.LastName.Contains(searchTerm) ||
                    s.Age.ToString().Contains(searchTerm) ||
                    s.Group.Contains(searchTerm));
        }

        var keySelector = GetSortProperty(sortColumn);

        studentsQuery = sortOrder?.ToLower() == "desc"
            ? studentsQuery.OrderByDescending(keySelector)
            : studentsQuery.OrderBy(keySelector);

        return studentsQuery;
    }

    private static Expression<Func<Student, object>> GetSortProperty(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "first-name" => s => s.FirstName,
            "last-name" => s => s.LastName,
            "age" => s => s.Age,
            "group" => s => s.Group,
            _ => s => s.Id
        };
    }
}
