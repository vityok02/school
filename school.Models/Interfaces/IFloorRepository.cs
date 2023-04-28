namespace SchoolManagement.Models.Interfaces;

public interface IFloorRepository : IRepository<Floor>
{
    public Floor? GetFloor(int id);
    public IEnumerable<Floor> GetFloors(int schoolId);
}
