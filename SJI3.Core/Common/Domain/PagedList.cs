using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SJI3.Core.Common.Domain;

public class PagedList<TEntity> : List<TEntity>
{
    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    private PagedList(IEnumerable<TEntity> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        AddRange(items);
    }

    public static PagedList<TEntity> Create(IQueryable<TEntity> source, int pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PagedList<TEntity>(items, count, pageNumber, pageSize);
    }

    public static PagedList<TEntity> Create(IEnumerable<TEntity> source, int totalCount, int pageNumber, int pageSize)
    {
        return new PagedList<TEntity>(source, totalCount, pageNumber, pageSize);
    }
}