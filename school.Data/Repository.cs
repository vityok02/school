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

    public TEntity? Get(int id)
    {
        return _dbContext.Set<TEntity>().Find(id);
    }

    public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
        IQueryable<TEntity> entities = _dbContext.Set<TEntity>();

        if (orderBy is not null)
        {
            entities = orderBy(entities);
        }

        return entities.ToArray();
    }

    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
        var entities = _dbContext.Set<TEntity>().Where(predicate);
        
        if (orderBy is not null)
        {
            entities = orderBy(entities);
        }    

        return entities.ToArray();
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
