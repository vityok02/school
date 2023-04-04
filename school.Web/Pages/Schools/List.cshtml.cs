using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolListModel : BasePageModel
{
    private readonly IRepository<Employee> _employeeRepository;

    public IEnumerable<Address> Addresses { get; set; } = null!;
    public bool IsError { get; set; } = false;
    public bool IsFirst { get; set; } = false;
    public string NameSort { get; set; } = null!;
    public string CitySort { get; set; } = null!;
    public string StreetSort { get; set; } = null!;
    public string FilterByParam { get; set; } = null!;
    public Dictionary<string, string> NameParams { get; set; } = null!;
    public Dictionary<string, string> CityParams { get; set; } = null!;
    public Dictionary<string, string> StreetParams { get; set; } = null!;

    public SchoolListModel(ISchoolRepository schoolRepository, IRepository<Employee> empRepository)
        : base(schoolRepository)
    {
        _employeeRepository = empRepository;
    }

    public IActionResult OnGet(string orderBy, string filterByParam)
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    var school = new School()
        //    {
        //        Name = i.ToString(),
        //        Address = new Address(i.ToString(), i.ToString(), i.ToString(), 0),
        //        OpeningDate = DateTime.Now
        //    };
        //    SchoolRepository.Add(school);
        //}
        OrderBy = orderBy;  
        NameSort = String.IsNullOrEmpty(orderBy) ? "name_desc" : "";
        CitySort = orderBy == "city" ? "city_desc" : "city";
        StreetSort = orderBy == "street" ? "street_desc" : "street";

        FilterByParam = filterByParam;

        Schools = SchoolRepository.GetSchools(FilterBy(FilterByParam), Sort(orderBy));

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

    public IActionResult OnGetSelectSchool(int id, string orderBy, string filterBy)
    {
        OnGet(orderBy, filterBy);
        var school = SchoolRepository.Get(id);
        if(school is null)
        {
            IsError = true;
        }

        SelectedSchoolName = school!.Name;

        SetSchoolId(school!.Id);
        return Page();
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

    public IActionResult OnPostDelete(int id)
    {
        var school = SchoolRepository.Get(id);
        if (school is null)
        {
            return RedirectToPage("List");
        }

        //var employees = _employeeRepository.GetAll(e => e.SchoolId == id);
        //foreach (var emp in employees)
        //{
        //    school.Employees.Clear();
        //}

        SchoolRepository.Delete(school);
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