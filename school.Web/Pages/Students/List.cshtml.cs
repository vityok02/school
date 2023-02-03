using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class ListModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public IEnumerable<Student> Students { get; set; } = null!;
    public string FirstNameSort { get; set; } = null!;
    public string LastNameSort { get; set; } = null!;
    public string AgeSort { get; set; } = null!;
    public string FilterByGroup { get; set; }

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

        FirstNameSort = String.IsNullOrEmpty(orderBy) ? "name_desc" : "";
        LastNameSort = orderBy == "lastName" ? "lastName_desc" : "lastName";
        AgeSort = orderBy == "age" ? "age_desc" : "age";

        Students = _studentRepository.GetAll(s => s.SchoolId == schoolId, Sort(orderBy));

        Students = FilterBy(filterByName, filterByAge, filterByGroup);
        
        return Page();

        static Func<IQueryable<Student>, IOrderedQueryable<Student>> Sort(string orderBy)
        {
            return orderBy switch
            {
                "name_desc" => s => s.OrderByDescending(s => s.FirstName),
                "lastName" => s => s.OrderBy(e => e.LastName),
                "lastName_desc" => s => s.OrderByDescending(e => e.LastName),
                "age" => s => s.OrderBy(e => e.Age),
                "age_desc" => s => s.OrderByDescending(e => e.Age),
                "group" => s => s.OrderBy(s => s.Group),
                _ => s => s.OrderBy(s => s.FirstName),
            };
        }
    }

    private IEnumerable<Student> FilterBy(string filterByName, int filterByAge, string filterByGroup)
    {
        if (filterByName is not null)
        {
            Students = Students.Where(s => s.FirstName.Contains(filterByName) || s.LastName.Contains(filterByName));
        }
        else if (filterByAge != 0)
        {
            Students = Students.Where(s => s.Age == filterByAge);
        }
        else if (filterByGroup is not null)
        {
            Students = Students.Where(s => s.Group == filterByGroup);
        }
        else
        {
            Message = "No matching results";
        }
        return Students;
    }

    private IEnumerable<Student> FilterBy(string filterBy)
    {
        if (filterBy is not null)
        {
            Students = Students.Where(s => s.FirstName == filterBy
            || s.LastName == filterBy
            || s.Age.ToString() == filterBy
            || s.Group == filterBy);
        }
        else
        {
            ViewData["EmptyMessage"] = "No matching results";
        }
        return Students;
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
}
