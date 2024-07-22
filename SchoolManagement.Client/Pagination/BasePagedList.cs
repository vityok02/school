﻿namespace SchoolManagement.Client.Pagination;

public class BasePagedList
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
    public int LastPage => TotalCount / PageSize + 1;
}
