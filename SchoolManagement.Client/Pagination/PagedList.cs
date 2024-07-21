namespace SchoolManagement.Client.Pagination;

public class PagedList<T> : BasePagedList
{
    public IEnumerable<T> Items { get; set; } = [];
}
