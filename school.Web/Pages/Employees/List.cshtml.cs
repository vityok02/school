using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Employees;

public class ListModel : BasePageModel
{
    private readonly IRepository<Employee> _employeeRepository;

    public IEnumerable<Employee> Employees { get; private set; } = null!;
    public string JobSort { get; set; } = null!;
    public string FilterByJob { get; set; } = null!;
    public IDictionary<string, string> JobParams { get; set; } = null!;

    public ListModel(IRepository<School> schoolRepository, IRepository<Employee> employeeRepository)
        :base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult OnGet(string orderBy, string filterByName, int filterByAge, string filterByJob)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        FirstNameSort = String.IsNullOrEmpty(orderBy) ? "firstName_desc" : "";
        LastNameSort = orderBy == "lastName" ? "lastName_desc" : "lastName";
        AgeSort = orderBy == "age" ? "age_desc" : "age";
        JobSort = orderBy == "job" ? "group_desc" : "group";

        FilterByName = filterByName;
        FilterByAge= filterByAge;
        FilterByJob = filterByJob;

        Employees = _employeeRepository.GetAll(FilterBy(FilterByName, FilterByAge, FilterByJob, schoolId), Sort(orderBy));

        var filterParams = GetFilters();

        FilterParams = new Dictionary<string, string>(filterParams)
        {
            { nameof(orderBy), orderBy},
        };

        FirstNameParams = new Dictionary<string, string>(filterParams)
        {
            {nameof(orderBy), FirstNameSort }
        };

        LastNameParams = new Dictionary<string, string>(filterParams)
        {
            { nameof(orderBy), LastNameSort }
        };

        AgeParams = new Dictionary<string, string>(filterParams)
        {
            { nameof(orderBy), JobSort }
        };
        JobParams = new Dictionary<string, string>(filterParams)
        {
            { nameof(orderBy), JobSort }
        };
        return Page();

        static Expression<Func<Employee, bool>> FilterBy(string filterByName, int filterByAge, string filterByJob, int schoolId)
        {
            return emp => emp.SchoolId == schoolId
            && (string.IsNullOrEmpty(filterByName) || emp.FirstName.Contains(filterByName))
            && (string.IsNullOrEmpty(filterByJob) || emp.Job.Contains(filterByJob))
            && (filterByAge == 0 || emp.Age == filterByAge);
        }

        static Func<IQueryable<Employee>, IOrderedQueryable<Employee>> Sort(string orderBy)
        {
            return orderBy switch
            {
                "firstName_desc" => e => e.OrderByDescending(e => e.FirstName),
                "lastName" => e => e.OrderBy(e => e.LastName),
                "lastName_desc" => e => e.OrderByDescending(e => e.LastName),
                "age" => e => e.OrderBy(e => e.Age),
                "age_desc" => e => e.OrderByDescending(e => e.Age),
                "group" => e => e.OrderBy(e => e.Job),
                "group_desc" => e => e.OrderByDescending(e => e.Job),
                _ => e => e.OrderBy(e => e.FirstName),
            };
        }
    }

    public IActionResult OnPostDelete(int id)
    {
        var employee = _employeeRepository.Get(id);
        if (employee is null)
        {
            return RedirectToPage("List");
        }

        _employeeRepository.Delete(employee!);

        return RedirectToPage("List");
    }

    private IDictionary<string, string> GetFilters()
    {
        var filterParams = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(FilterByName))
        {
            filterParams.Add(nameof(FilterByName), FilterByName);
        }

        if(FilterByAge > 0)
        {
            filterParams.Add(nameof(FilterByAge), FilterByAge.ToString());
        }

        if(!string.IsNullOrWhiteSpace(FilterByJob))
        {
            filterParams.Add(nameof(FilterByJob), FilterByJob);
        }

        return filterParams;
    }
}