namespace SJI3.Core.Features.Common;

public class PaginationMetadata
{
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public int CurrentPage { get; init; }
    public bool HasNext { get; init; }
    public bool HasPrevious { get; init; }
}