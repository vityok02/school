using SchoolManagement.Client.Features.Schools.Dtos;

namespace SchoolManagement.Client.Features.Schools;

public interface ISchoolService
{
    Task<IEnumerable<SchoolItem>?> GetSchools();
    Task<School> GetSchool(int schoolId);
    Task<int> CreateSchool(School school);
    Task UpdateSchool(School school);
    Task DeleteSchool(int schoolId);
}
