using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.API;

public class PagedList<T>
{
    public IEnumerable<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int PageCount { get; }
    public int TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;

    private PagedList(IEnumerable<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        PageCount = (int)Math.Ceiling(totalCount /(double) pageSize);
        TotalCount = totalCount;
    }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        int totalCount = await query.CountAsync();

        if (page < 1)
        {
            page = 1;
        }

        var items = await query
            .Skip((page-1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync();

        return new(items, page, pageSize, totalCount);
    }
}
