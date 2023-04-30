using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class EditModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public StudentDto? StudentDto { get; set; } = null!;

    public EditModel(ISchoolRepository schoolRepository, IRepository<Student> studentRepository)
        :base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var student = await _studentRepository.GetAsync(id);
        if (student is null)
        {
            return RedirectToPage("List");
        }

        StudentDto = student.ToStudentDto();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(StudentDto studentDto)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var students = await _studentRepository.GetAllAsync(s => s.SchoolId == schoolId);

        if ((students.Where(s => s.FirstName == studentDto.FirstName
            && s.LastName == studentDto.LastName
            && s.Age == studentDto.Age
            && s.Id != studentDto.Id).Any()))
        {
            ErrorMessage = "Such student already exists";
            return Page();
        }

        var student = await _studentRepository.GetAsync(studentDto.Id);

        student!.UpdateInfo(studentDto.FirstName, studentDto.LastName, studentDto.Age, studentDto.Group);

        await _studentRepository.UpdateAsync(student);
        return RedirectToPage("List");
    }
}