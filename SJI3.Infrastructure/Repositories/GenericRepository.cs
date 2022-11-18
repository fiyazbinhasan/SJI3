using System;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SJI3.Core.Common.Infra;

namespace SJI3.Infrastructure.Repositories;

public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : Entity<TKey>
{
    private readonly DbContext _context;

    public GenericRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(TKey id, string includeProperties = "")
    {
        IQueryable<T> query = _context.Set<T>();
        
        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(a => a.Id.Equals(id));
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void AddRangeAsync(List<T> entities)
    {
        _context.Set<T>().AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void UpdateRange(List<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>();
    }
}