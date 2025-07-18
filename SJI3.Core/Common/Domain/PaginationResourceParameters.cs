﻿namespace SJI3.Core.Common.Domain;

public abstract class PaginationResourceParameters
{
    private const int MaxPageSize = 20;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}