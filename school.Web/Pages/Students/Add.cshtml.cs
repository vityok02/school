using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class StudentFormModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public AddStudentDto? StudentDto { get; private set; } = null!;

    public StudentFormModel(ISchoolRepository schoolRepository, IRepository<Student> studentRepository)
        :base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IActionResult> OnPostAsync(AddStudentDto studentDto)
    {
        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var students = await _studentRepository
            .GetAllAsync(s => s.SchoolId == SelectedSchoolId
            && s.FirstName == studentDto.FirstName
            && s.LastName == studentDto.LastName
            && s.Age == studentDto.Age);

        if (students.Any())
        {
            ErrorMessage = "Such student already exist";
            return Page();
        }

        var school = await SchoolRepository.GetAsync(SelectedSchoolId);
        if (school is null)
        {
            return RedirectToSchoolList();
        }

        Student student = new(studentDto.FirstName, studentDto.LastName, studentDto.Age, studentDto.Group)
        {
            School = school!
        };

        await _studentRepository.AddAsync(student);
        return RedirectToPage("List");
    }
}
