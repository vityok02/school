namespace SchoolManagement.Models.Interfaces;

public interface IFloorRepository : IRepository<Floor>
{
    Task<Floor> GetFloorAsync(int id);
    Task<IEnumerable<Floor>> GetSchoolFloorsAsync(int schoolId);
}
