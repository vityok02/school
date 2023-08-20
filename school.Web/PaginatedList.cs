namespace SchoolManagement.Web;

public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public int FirstPage => 1;
    public int LastPage => TotalPages;
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    public static int PageSize => 10;

    public PaginatedList(IEnumerable<T> items, int pageIndex)
    {
        var count = items.Count();

        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)PageSize);

        AddRange(items.Skip(
            (pageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToList());
    }
}
