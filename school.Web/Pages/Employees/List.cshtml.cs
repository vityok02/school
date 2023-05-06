using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Employees;

public class ListModel : BasePageModel
{
    private readonly IEmployeeRepository _employeeRepository;

    public IEnumerable<EmployeeItemDto> EmployeeItems { get; set; } = default!;
    public string PositionSort { get; set; } = default!;
    public string FilterByPosition { get; set; } = default!;
    public IDictionary<string, string> PositionParams { get; set; } = default!;

    public ListModel(ISchoolRepository schoolRepository, IEmployeeRepository employeeRepository)
        : base(schoolRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IActionResult> OnGetAsync(string orderBy, string filterByName, int filterByAge, string filterByPosition)
    {
        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var school = await SchoolRepository.GetAsync(SelectedSchoolId);
        if (school is null)
        {
            return RedirectToSchoolList();
        }

        FirstNameSort = String.IsNullOrEmpty(orderBy) ? "firstName_desc" : "";
        LastNameSort = orderBy == "lastName" ? "lastName_desc" : "lastName";
        AgeSort = orderBy == "age" ? "age_desc" : "age";
        PositionSort = orderBy == "position" ? "position_desc" : "position";

        FilterByName = filterByName;
        FilterByAge= filterByAge;
        FilterByPosition = filterByPosition;

        var employees = await _employeeRepository.GetSchoolEmployeesAsync(FilterBy(FilterByName, FilterByAge, FilterByPosition),
            Sort(orderBy), SelectedSchoolId);

        EmployeeItems = employees.Select(s => s.ToEmployeeItemDto()).ToArray();

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
            { nameof(orderBy), AgeSort }
        };
        PositionParams = new Dictionary<string, string>(filterParams)
        {
            { nameof(orderBy), PositionSort }
        };

        static Expression<Func<Employee, bool>> FilterBy(string filterByName, int filterByAge, string filterByPosition)
        {
            return emp => (string.IsNullOrEmpty(filterByName) || emp.FirstName.Contains(filterByName))
                && (string.IsNullOrEmpty(filterByPosition) || emp.Positions.Any(p => p.Name.Contains(filterByPosition)))
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
                "position" => e => e.OrderBy(e => e.Positions.FirstOrDefault()!.Name),
                "position_desc" => e => e.OrderByDescending(e => e.Positions.FirstOrDefault()!.Name),
                _ => e => e.OrderBy(e => e.FirstName),
            };
        }

        return Page();
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

        if (!string.IsNullOrWhiteSpace(FilterByPosition))
        {
            filterParams.Add(nameof(FilterByPosition), FilterByPosition);
        }

        return filterParams;
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var employee = await _employeeRepository.GetAsync(id);
        if (employee is null)
        {
            return RedirectToPage("List");
        }
        
        await _employeeRepository.DeleteAsync(employee!);

        return RedirectToPage("List");
    }
}