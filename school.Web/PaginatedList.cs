namespace SchoolManagement.Web;

public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    public static int PageSize => 10;

    public PaginatedList(List<T> items, int count, int pageIndex)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)PageSize);

        AddRange(items);
    }

    public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex)
    {
        var count = source.Count();
        var items = source.Skip(
            (pageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        return new PaginatedList<T>(items, count, pageIndex);
    }
}
