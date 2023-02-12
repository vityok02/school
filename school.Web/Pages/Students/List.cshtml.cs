using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Students;

public class ListModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public IEnumerable<Student> Students { get; set; } = null!;
    public string GroupSort { get; set; } = null!;
    public string FilterByGroup { get; set; } = null!;
    public IDictionary<string, string> GroupParams { get; set; } = null!;

    public ListModel(IRepository<School> schoolRepository, IRepository<Student> studentRepository)
        : base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnGet(string orderBy, string filterByName, int filterByAge, string filterByGroup)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        OrderBy = orderBy;
        FirstNameSort = String.IsNullOrEmpty(orderBy) ? "firstName_desc" : "";
        LastNameSort = orderBy == "lastName" ? "lastName_desc" : "lastName";
        AgeSort = orderBy == "age" ? "age_desc" : "age";
        GroupSort = orderBy == "group" ? "group_desc" : "group";

        FilterByName = filterByName;
        FilterByGroup = filterByGroup;
        FilterByAge = filterByAge;

        Students = _studentRepository.GetAll(FilterBy(FilterByName, FilterByAge, FilterByGroup, schoolId),
            Sort(orderBy));

        if(!Students.Any())
        {
            Message = "Not found";
        }

        var filterParams = GetFilters();

        FilterParams = new Dictionary<string, string>(filterParams)
        {
            { nameof(orderBy), orderBy }
        };

        FirstNameParams = new Dictionary<string, string>(filterParams)
        {
            { nameof(orderBy), FirstNameSort }
        };

        LastNameParams = new Dictionary<string, string>(filterParams)
        {
            { nameof(orderBy), LastNameSort }
        };

        AgeParams = new Dictionary<string, string>(filterParams)
        {
            { nameof(orderBy), AgeSort }
        };

        GroupParams = new Dictionary<string, string>(filterParams)
        {
            { nameof(orderBy), GroupSort }
        };

        return Page();

        static Expression<Func<Student, bool>> FilterBy(string filterByName, int filterByAge, string filterByGroup, int schoolId)
        {
            return student => student.SchoolId == schoolId
                && (string.IsNullOrEmpty(filterByName) || student.FirstName.Contains(filterByName))
                && (string.IsNullOrEmpty(filterByGroup) || student.Group.Contains(filterByGroup))
                && (filterByAge == 0 || student.Age == filterByAge);
        }

        static Func<IQueryable<Student>, IOrderedQueryable<Student>> Sort(string orderBy)
        {
            return orderBy switch
            {
                "firstName_desc" => s => s.OrderByDescending(s => s.FirstName),
                "lastName" => s => s.OrderBy(s => s.LastName),
                "lastName_desc" => s => s.OrderByDescending(s => s.LastName),
                "age" => s => s.OrderBy(s => s.Age),
                "age_desc" => s => s.OrderByDescending(s => s.Age),
                "group" => s => s.OrderBy(s => s.Group),
                "group_desc" => s => s.OrderByDescending(s => s.Group), 
                _ => s => s.OrderBy(s => s.FirstName),
            };
        }
    }


    public IActionResult OnPostDelete(int id)
    {
        var student = _studentRepository.Get(id);
        if (student is null)
        {
            return RedirectToPage("List");
        }

        _studentRepository.Delete(student);
        return RedirectToPage("List");
    }

    private IDictionary<string, string> GetFilters()
    {
        var filterParams = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(FilterByName))
        {
            filterParams.Add(nameof(FilterByName), FilterByName);
        }

        if (FilterByAge > 0)
        {
            filterParams.Add(nameof(FilterByAge), FilterByAge.ToString());
        }

        if (!string.IsNullOrWhiteSpace(FilterByGroup))
        {
            filterParams.Add(nameof(FilterByGroup), FilterByGroup);
        }

        return filterParams;
    }
}