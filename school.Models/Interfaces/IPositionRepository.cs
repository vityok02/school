namespace SchoolManagement.Models.Interfaces;

public interface IPositionRepository : IRepository<Position>
{
    Task<IEnumerable<Position>> GetSchoolPositionsAsync(int schoolId);
    Task<IEnumerable<Position>> GetUnSelectedPositionsAsync(int schoolId);
    Task<Position> GetPositionAsync(int positionId);
    Task<IEnumerable<Position>> GetEmployeePositions(int[] checkedPositionsId);
}
