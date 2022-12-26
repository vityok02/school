using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class ListModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public IEnumerable<Student>? Students { get; private set; }

    public ListModel(IRepository<Student> studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnGet()
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        Students = _studentRepository.GetAll(s => s.SchoolId == schoolId);
        return Page();
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
