using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Data;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        _dbContext.SaveChanges();
    }
    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
    }
    public void Delete(TEntity entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
    }
    public IEnumerable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>().ToArray();
    }
    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbContext.Set<TEntity>().Where(predicate).ToArray();
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
