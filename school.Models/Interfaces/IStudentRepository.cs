namespace SchoolManagement.Models.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    IQueryable<Student> GetStudentsQuery(
        int schoolId, string? searchTerm, string? sortColumn, string? sortOrder);
}
