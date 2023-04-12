using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class StudentFormModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public StudentDto StudentDto { get; private set; } = default!;

    public StudentFormModel(ISchoolRepository schoolRepository, IRepository<Student> studentRepository)
        :base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnPost(StudentDto studentDto)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var students = _studentRepository
            .GetAll(s => s.SchoolId == schoolId 
            && s.FirstName == studentDto.FirstName
            && s.LastName == studentDto.LastName
            && s.Age == studentDto.Age);

        if (students.Any())
        {
            ErrorMessage = "Such student already exist";
            return Page();
        }

        var school = SchoolRepository.Get(schoolId);
        if (school is null)
        {
            return RedirectToSchoolList();
        }

        Student student = new(studentDto.FirstName, studentDto.LastName, studentDto.Age, studentDto.Group)
        {
            School = school!
        };

        _studentRepository.Add(student);
        return RedirectToPage("List");
    }
}
