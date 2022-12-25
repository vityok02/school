using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class StudentFormModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<School> _schoolRepository;

    public IEnumerable<School>? Schools { get; set; }
    public string? Message { get; private set; } = "";

    public StudentFormModel(IRepository<Student> studentRepository, IRepository<School> schoolRepository)
    {
        _studentRepository = studentRepository;
        _schoolRepository = schoolRepository;
    }

    public IActionResult OnPost(string firstName, string lastName, int age, string group)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var students = _studentRepository.GetAll(s => s.SchoolId == schoolId);
        if (students.Any(s => s.FirstName == firstName
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
