using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolsListModel : BaseListPageModel
{
    public override string ListPageUrl => "/Schools/List";
    public string NameSort { get; private set; } = default!;
    public string CitySort { get; private set; } = default!;
    public string StreetSort { get; private set; } = default!;
    public string FilterByParam { get; private set; } = default!;
    public Dictionary<string, string> NameParams { get; private set; } = default!;
    public Dictionary<string, string> CityParams { get; private set; } = default!;
    public Dictionary<string, string> StreetParams { get; private set; } = default!;
    public bool HasSchools => Items.Any();

    public SchoolsListModel(ISchoolRepository schoolRepository)
        : base(schoolRepository) { }

    public async Task<IActionResult> OnGet(string orderBy, string filterByParam, int? pageIndex)
    {

        OrderBy = orderBy;
        NameSort = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";
        CitySort = orderBy == "city" ? "city_desc" : "city";
        StreetSort = orderBy == "street" ? "street_desc" : "street";
        FilterByParam = filterByParam;

        var schoolsFromDb = await SchoolRepository
            .GetSchools(
                FilterBy(FilterByParam),
                Sort(orderBy));

        var schoolItemDtos = schoolsFromDb
            .Select(s => s.ToSchoolItemDto())
            .ToArray();

        Items = new PaginatedList<object>(schoolItemDtos.Cast<object>(), PageIndex = pageIndex ?? 1);

        var filterParams = GetFilters();

        FilterParams = new Dictionary<string, string>(filterParams)
        {
            {nameof(orderBy), orderBy }
        };

        NameParams = new Dictionary<string, string>(filterParams)
        {
            {nameof(orderBy), NameSort }
        };

        CityParams = new Dictionary<string, string>(filterParams)
        {
            {nameof(orderBy), CitySort }
        };

        StreetParams = new Dictionary<string, string>(filterParams)
        {
            {nameof(orderBy), StreetSort }
        };

        return Page();
    }

    public IActionResult OnPostSetSchool(int id, string filterByParam, string orderBy, int pageIndex)
    {
        if (id == 0)
        {
            return RedirectToPage("List");
        }

        SetSchoolId(id);
        return RedirectToPage("List");
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var school = await SchoolRepository.GetByIdAsync(id);
        if (school is null)
        {
            return RedirectToPage("List");
        }

        await SchoolRepository.DeleteAsync(school);
        return RedirectToPage("List");
    }

    private IDictionary<string, string> GetFilters()
    {
        var filterParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(FilterByParam))
        {
            filterParams.Add(nameof(FilterByParam), FilterByParam);
        }

        return filterParams;
    }

    private static Func<IQueryable<School>, IOrderedQueryable<School>> Sort(string orderBy)
    {
        return orderBy switch
        {
            "name_desc" => s => s.OrderByDescending(s => s.Name),
            "city" => s => s.OrderBy(s => s.Address.City),
            "city_desc" => s => s.OrderByDescending(s => s.Address.City),
            "street" => s => s.OrderBy(s => s.Address.Street),
            "street_desc" => s => s.OrderByDescending(s => s.Address.Street),
            _ => s => s.OrderBy(s => s.Name),
        };
    }

    private static Expression<Func<School, bool>> FilterBy(string filterBy)
    {
        return s => (string.IsNullOrEmpty(filterBy)
            || (s.Name.Contains(filterBy)
            || s.Address.City.Contains(filterBy)
            || s.Address.Street.Contains(filterBy)));
    }
}