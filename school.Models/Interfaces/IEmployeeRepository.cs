namespace SchoolManagement.Models.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    IEnumerable<Employee> GetSchoolEmployees(int schoolId, string filterByName, int filterByAge, string filterByJob);
}
