using SchoolManagement.API.Features.Schools.Dtos;

namespace SchoolManagement.API.Features.Schools;

public interface ISchoolService
{
    Task<PagedList<SchoolDto>> GetSchools(
        string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize);
    Task<SchoolDto> GetSchoolById(int id);
    Task<CreatedLink<SchoolDto>> CreateSchool(HttpContext context, SchoolCreateDto school);
    //Task<SchoolUpdateDto> UpdateSchool(int id, SchoolUpdateDto school);
    Task DeleteSchool(int id);
}
