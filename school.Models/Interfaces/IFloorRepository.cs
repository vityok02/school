
namespace SchoolManagement.Models.Interfaces;

public interface IFloorRepository : IRepository<Floor>
{
    Task<Floor> GetFloor(int id);
    Task<Floor> GetSchoolFloorAsync(int schoolId, int floorId);
    Task<IEnumerable<Floor>> GetFloorsAsync(int schoolId);
    IQueryable<Floor> GetFloorsQuery(int schoolId, string? sortOrder);
}
