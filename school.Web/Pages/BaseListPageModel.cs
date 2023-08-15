using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages;

public abstract class BaseListPageModel : BasePageModel
{
    public PaginatedList<object> Items { get; protected set; } = default!;

    public abstract string ListPageUrl { get; }

    protected BaseListPageModel(ISchoolRepository schoolRepository)
        : base(schoolRepository)
    {
    }
}
