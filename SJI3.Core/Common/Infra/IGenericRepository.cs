using System;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SJI3.Core.Common.Infra;

public interface IGenericRepository<T, in TKey> where T : Entity<TKey>
{
    IQueryable<T> Query();
    
    Task<T> GetByIdAsync(TKey id, string includeProperties = "");

    Task<IReadOnlyList<T>> ListAllAsync();

    Task AddAsync(T entity);

    void AddRangeAsync(List<T> entities);

    void Update(T entity);

    void UpdateRange(List<T> entities);

    void Delete(T entity);
}