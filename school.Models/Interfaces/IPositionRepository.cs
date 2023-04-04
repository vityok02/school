namespace SchoolManagement.Models.Interfaces;

public interface IPositionRepository : IRepository<Position>
{
    public IEnumerable<Position> GetSchoolPositions(int schoolId);
    public IEnumerable<Position> GetUnSelectedPositions(int schoolId);
    public Position GetPosition(int positionId);
}
