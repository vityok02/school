using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Students;

public class StudentsListModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public IEnumerable<StudentDto>? StudentsDto { get; private set; } = default!;
    public PaginatedList<StudentDto> Items { get; private set; } = default!;
    public string ListPageUrl => "/Students/List";
    public string GroupSort { get; private set; } = default!;
    public string FilterByGroup { get; private set; } = default!;
    public IDictionary<string, string> GroupParams { get; private set; } = default!;
    public bool HasStudents => Items.Any();

    public StudentsListModel(ISchoolRepository schoolRepository, IRepository<Student> studentRepository)
        : base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IActionResult> OnGet(
        string orderBy,
        string filterByName,
        int filterByAge,
        string filterByGroup,
        int? pageIndex)
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

        OrderBy = orderBy;
        FirstNameSort = String.IsNullOrEmpty(orderBy) ? "firstName_desc" : "";
        LastNameSort = orderBy == "lastName" ? "lastName_desc" : "lastName";
        AgeSort = orderBy == "age" ? "age_desc" : "age";
        GroupSort = orderBy == "group" ? "group_desc" : "group";

        FilterByName = filterByName;
        FilterByGroup = filterByGroup;
        FilterByAge = filterByAge;

        var students = await _studentRepository.GetAllAsync(FilterBy(FilterByName, FilterByAge, FilterByGroup, SelectedSchoolId),
            Sort(orderBy));
        StudentsDto = students.Select(s => s.ToStudentDto()).ToArray();

        Items = new PaginatedList<StudentDto>(StudentsDto, PageIndex = pageIndex ?? 1);

        if(!StudentsDto.Any())
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
                && (string.IsNullOrEmpty(filterByName) || student.FirstName.Contains(filterByName)
                || string.IsNullOrEmpty(filterByName) || student.LastName.Contains(filterByName))
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

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var student = await _studentRepository.GetAsync(id);
        if (student is null)
        {
            return RedirectToPage("List");
        }

        await _studentRepository.DeleteAsync(student);
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