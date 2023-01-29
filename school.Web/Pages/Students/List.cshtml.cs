using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class ListModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public IEnumerable<Student> Students { get; private set; } = null!;
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

        Students = _studentRepository.GetAll(s => s.SchoolId == schoolId, Sort(orderBy));

        return Page();

        Func<IQueryable<Student>, IOrderedQueryable<Student>> Sort(string orderBy)
        {
            return orderBy switch
            {
                "firstName" => s => s.OrderBy(s => s.FirstName),
                "lastName" => s => s.OrderBy(s => s.LastName),
                "age" => s => s.OrderBy(s => s.Age),
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
