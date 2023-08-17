using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class EditStudentModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;
    private readonly IValidator<IStudentDto> _validator;

    public StudentDto StudentDto { get; private set; } = default!;

    public EditStudentModel(
        ISchoolRepository schoolRepository,
        IRepository<Student> studentRepository,
        IValidator<IStudentDto> validator)
        : base(schoolRepository)
    {
        _studentRepository = studentRepository;
        _validator = validator;
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
        var validationResult = await _validator.ValidateAsync(studentDto);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, nameof(StudentDto));

            StudentDto = studentDto;

            return Page();
        }

        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var students = await _studentRepository.GetAllAsync(s => s.SchoolId == SelectedSchoolId);

        if (students.Any(s => s.FirstName == studentDto.FirstName
            && s.LastName == studentDto.LastName
            && s.Age == studentDto.Age
            && s.Id != studentDto.Id))
        {
            StudentDto = studentDto;

            ErrorMessage = "Such student already exists";

            return Page();
        }

        var student = await _studentRepository.GetAsync(studentDto.Id);

        student!.UpdateInfo(studentDto.FirstName, studentDto.LastName, studentDto.Age, studentDto.Group);

        await _studentRepository.UpdateAsync(student);
        return RedirectToPage("List");
    }
}