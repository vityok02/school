﻿using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages;

public abstract class BaseListPageModel : BasePageModel
{
    public PaginatedList<object> Items { get; protected set; } = default!;
    public string OrderBy { get; protected set; } = default!;
    public string FirstNameSort { get; protected set; } = default!;
    public string LastNameSort { get; protected set; } = default!;
    public string AgeSort { get; protected set; } = default!;
    public string FilterByName { get; protected set; } = default!;
    public int FilterByAge { get; protected set; }
    public IDictionary<string, string> FilterParams { get; protected set; } = default!;
    public IDictionary<string, string> FirstNameParams { get; protected set; } = default!;
    public IDictionary<string, string> LastNameParams { get; protected set; } = default!;
    public IDictionary<string, string> AgeParams { get; protected set; } = default!;
    public int PageIndex { get; protected set; }
    public virtual string ListPageUrl { get; protected set; } = "List";

    protected BaseListPageModel(ISchoolRepository schoolRepository)
        : base(schoolRepository)
    {
    }
}
