using SchoolManagement.Client.Features.Schools.Dtos;
using SchoolManagement.Client.Pagination;

namespace SchoolManagement.Client.Features.Schools;

public interface ISchoolService
{
    Task<PagedList<School>> GetSchools(
        string? searchTerm, string? sortColumn, string? sortOrder, int? page, int? pageSize);
    Task<School> GetSchool(int schoolId);
    Task<int> CreateSchool(School school);
    Task UpdateSchool(School school);
    Task DeleteSchool(int schoolId);
}
