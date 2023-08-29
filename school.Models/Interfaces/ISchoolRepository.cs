﻿using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface ISchoolRepository : IRepository<School>
{
    Task<School> GetSchoolAsync(int id);
    Task<IEnumerable<School>> GetSchools(Expression<Func<School, bool>> predicate = null!,
        Func<IQueryable<School>, IOrderedQueryable<School>> orderBy = null!);
}
