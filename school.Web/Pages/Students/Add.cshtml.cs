using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Data;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Web.Pages.Students;

public class StudentFormModel : PageModel
{
    private readonly IRepository<Student> _studentRepository;
    public readonly IRepository<School> _schoolRepository;
    public IEnumerable<School>? Schools { get; private set; }
    public string? Message { get; set; } = "";
    public StudentFormModel(IRepository<Student> studentRepository, IRepository<School> schoolRepository)
    {
        _studentRepository = studentRepository;
        _schoolRepository = schoolRepository;
    }
    public void OnGet()
    {

    }
    public IActionResult OnPost(string firstName, string lastName, int age, string group)
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];

        if (!int.TryParse(sId, out var schoolId))
        {
            return NotFound("School not found");
        }

        var students = _studentRepository.GetAll(s => s.SchoolId == schoolId);

        if(students.Any(s => s.FirstName == firstName 
            && s.LastName == lastName
            && s.Age == age))
        {
            Message = "Such student already exist";
            return Page();
        }

        Student student = new(firstName, lastName, age, group);

        var school = _schoolRepository.Get(schoolId);
        student.School = school!;

        _studentRepository.Add(student);
        return RedirectToPage("List");
    }
}
