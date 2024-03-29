﻿namespace SchoolManagement.Models.Interfaces;

public interface IFloorRepository : IRepository<Floor>
{
    Task<Floor> GetFloorAsync(int id);
    Task<IEnumerable<Floor>> GetFloorsAsync(int schoolId);
    Task<Floor> GetSchoolFloorAsync(int schoolId, int floorId);
}
