using System.Linq.Expressions;

namespace school.Models.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    void Add(TEntity entity);
    void Delete(TEntity entity);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
    void SaveChanges();
    void Update(TEntity entity);
}
