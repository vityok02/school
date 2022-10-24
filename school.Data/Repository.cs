﻿using Microsoft.EntityFrameworkCore;
using school.Models;
using System.Linq.Expressions;

namespace school.Data;
public class Repository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        GetAll();
    }

    public void Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        _dbContext.SaveChanges();
    }
    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);
    }
    public void Delete(TEntity entity)
    {
        _dbContext.Remove(entity);
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
