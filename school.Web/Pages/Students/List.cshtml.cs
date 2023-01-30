using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class ListModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public IEnumerable<Student> Students { get; private set; } = null!;
    public string FirstNameSort { get; set; } = null!;
    public string LastNameSort { get; set; } = null!;
    public string AgeSort { get; set; } = null!;

    public ListModel(IRepository<School> schoolRepository, IRepository<Student> studentRepository)
        : base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnGet(string orderBy)
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

        return Page();

        Func<IQueryable<Student>, IOrderedQueryable<Student>> Sort(string orderBy)
        {
            return orderBy switch
            {
                "name_desc" => e => e.OrderByDescending(e => e.FirstName),
                "lastName" => e => e.OrderBy(e => e.LastName),
                "lastName_desc" => e => e.OrderByDescending(e => e.LastName),
                "age" => e => e.OrderBy(e => e.Age),
                "age_desc" => e => e.OrderByDescending(e => e.Age),
                "group" => s => s.OrderBy(s => s.Group),
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
}
