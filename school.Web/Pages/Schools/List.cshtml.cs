using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolListModel : BasePageModel
{
    public IEnumerable<SchoolItemDto> SchoolItems { get; private set; } = null!;
    public IEnumerable<Address> Addresses { get; private set; } = null!;
    public bool IsError { get; private set; } = false;
    public bool IsFirst { get; private set; } = false;
    public string NameSort { get; private set; } = null!;
    public string CitySort { get; private set; } = null!;
    public string StreetSort { get; private set; } = null!;
    public string FilterByParam { get; private set; } = null!;
    public Dictionary<string, string> NameParams { get; private set; } = null!;
    public Dictionary<string, string> CityParams { get; private set; } = null!;
    public Dictionary<string, string> StreetParams { get; private set; } = null!;

    public SchoolListModel(ISchoolRepository schoolRepository)
        : base(schoolRepository)
    {
    }

    public IActionResult OnGet(string orderBy, string filterByParam, bool error = false)
    {
        if (error == true)
        {
            OnError();
        }

        OrderBy = orderBy;  
        NameSort = String.IsNullOrEmpty(orderBy) ? "name_desc" : "";
        CitySort = orderBy == "city" ? "city_desc" : "city";
        StreetSort = orderBy == "street" ? "street_desc" : "street";

        FilterByParam = filterByParam;

        var schools = SchoolRepository.GetSchools(FilterBy(FilterByParam), Sort(orderBy));

        SchoolItems = schools.Select(s => s.ToSchoolItemDto()).ToArray();

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

        static Expression<Func<School, bool>> FilterBy(string filterBy)
        {
            return s => (string.IsNullOrEmpty(filterBy) 
                || (s.Name.Contains(filterBy) 
                || s.Address.City.Contains(filterBy) 
                || s.Address.Street.Contains(filterBy)));
        }

        static Func<IQueryable<School>, IOrderedQueryable<School>> Sort(string orderBy)
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
    }

    public void OnError()
    {

    }

    public void OnGetFirstTime(string orderBy, string filterBy)
    {
        IsFirst = true;
        OnGet(orderBy, filterBy);
    }

    public void OnGetError(string orderBy, string filterBy)
    {
        IsError = true;
        IsFirst = true;
        OnGet(orderBy, filterBy);
    }

    public async Task<IActionResult> OnGetSelectSchool(int id, string orderBy, string filterBy)
    {
        var school = await SchoolRepository.GetAsync(id);
        if(school is null)
        {
            IsError = true;
            return OnGet(orderBy, filterBy);
        }

        SelectedSchoolName = school!.Name;
        SelectedSchoolId = id;

        SetSchoolId(school!.Id);
        //OnGet(orderBy, filterBy);
        return OnGet(orderBy, filterBy);
    }

    public IActionResult OnPostSetSchool(int id)
    {
        if (id == 0)
        {
            return RedirectToPage("List");
        }

        SelectSchool(id);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var school = await SchoolRepository.GetAsync(id);
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
}