using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class EditModel : PageModel
{
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<School> _schoolRepository;
    public Student? Student { get; private set; }
    public string Message { get; set; } = "";

    public EditModel(IRepository<Student> studentRepository, IRepository<School> schoolRepository)
    {
        _studentRepository = studentRepository;
        _schoolRepository = schoolRepository;
    }

    public IActionResult OnGet(int id)
    {
        Student = _studentRepository.Get(id);

        if (Student is null)
        {
            return NotFound("Student not found");
        }

        return Page();
    }

    public IActionResult OnPost(int id, string firstName, string lastName, int age, string group)
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        if (!int.TryParse(sId, out int schoolId))
        {
            return NotFound("School not found");
        }

        var students = _studentRepository.GetAll(s => s.SchoolId == schoolId);

        if (students.Where(s => s.FirstName == firstName
            && s.LastName == lastName
            && s.Age == age).Count() > 1)
        {
            Message = "Such student already exists";
        }

        var student = _studentRepository.Get(id);

        student!.UpdateInfo(firstName, lastName, age);

        _studentRepository.Update(student);
        return RedirectToPage("List");
    }
}