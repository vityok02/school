﻿using System.Linq.Expressions;

namespace SchoolManagement.Models.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    TEntity? Get(int id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
    void SaveChanges();
}
