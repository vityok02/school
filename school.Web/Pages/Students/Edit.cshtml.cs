using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class EditModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public StudentDto StudentDto { get; set; } = default!;

    public EditModel(ISchoolRepository schoolRepository, IRepository<Student> studentRepository)
        :base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnGet(int id)
    {
        var student = _studentRepository.Get(id);
        if (student is null)
        {
            return RedirectToPage("List");
        }

        StudentDto = student.ToStudentDto();

        return Page();
    }

    public IActionResult OnPost(int id, StudentDto studentDto)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var students = _studentRepository.GetAll(s => s.SchoolId == schoolId);

        if ((students.Where(s => s.FirstName == studentDto.FirstName
            && s.LastName == studentDto.LastName
            && s.Age == studentDto.Age
            && s.Id != id).Any()))
        {
            ErrorMessage = "Such student already exists";
            return Page();
        }

        var student = _studentRepository.Get(id);

        student!.UpdateInfo(studentDto.FirstName, studentDto.LastName, studentDto.Age, studentDto.Group);

        _studentRepository.Update(student);
        return RedirectToPage("List");
    }
}