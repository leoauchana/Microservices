namespace Application.Common;

public class PagedResult<T>
{
    public List<T> Items { get; init; } = [];
    public int TotalRecords { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}