using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class ListModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    [BindProperty]
    public IEnumerable<Student> Students { get; set; } = null!;
    public string FirstNameSort { get; set; } = null!;
    public string LastNameSort { get; set; } = null!;
    public string AgeSort { get; set; } = null!;

    public ListModel(IRepository<School> schoolRepository, IRepository<Student> studentRepository)
        : base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnGet(IEnumerable<Student> students, string orderBy, string firstName = null!, string lastName = null!, int age = 0, string group = null!)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        FirstNameSort = String.IsNullOrEmpty(orderBy) ? "name_desc" : "";
        LastNameSort = orderBy == "lastName" ? "lastName_desc" : "lastName";
        AgeSort = orderBy == "age" ? "age_desc" : "age";

        Students = _studentRepository.GetAll(s => s.SchoolId == schoolId);

        Students = Filter(firstName, lastName, age, group);

        Students = Sort(orderBy);

        return Page();

        IEnumerable<Student> Sort(string orderBy)
        {
            return orderBy switch
            {
                "name_desc" => Students.OrderByDescending(e => e.FirstName),
                "lastName" => Students.OrderBy(e => e.LastName),
                "lastName_desc" => Students.OrderByDescending(e => e.LastName),
                "age" => Students.OrderBy(e => e.Age),
                "age_desc" => Students.OrderByDescending(e => e.Age),
                "group" => Students.OrderBy(s => s.Group),
                _ => Students.OrderBy(s => s.FirstName),
            };
        }
    }

    private IEnumerable<Student> Filter(string firstName, string lastName, int age, string group)
    {
        if (firstName is not null)
        {
            Students = Students.Where(s => s.FirstName == firstName);
        }
        if (lastName is not null)
        {
            Students = Students.Where(s => s.LastName == lastName);
        }
        if (age is not 0)
        {
            Students = Students.Where(s => s.Age == age);
        }
        if (group is not null)
        {
            Students = Students.Where(s => s.Group == group);
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
