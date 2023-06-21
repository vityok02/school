using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class AddModel : BaseStudentPageModel
{
    public AddStudentDto StudentDto { get; private set; } = default!;

    public AddModel(
        ISchoolRepository schoolRepository, 
        IRepository<Student> studentRepository,
        IValidator<IStudentDto> validator)
        : base(schoolRepository, studentRepository, validator)
    { }

    public async Task<IActionResult> OnPostAsync(AddStudentDto studentDto)
    {
        var validationResult = await _validator.ValidateAsync(studentDto);

        if(!validationResult.IsValid) 
        {
            validationResult.AddToModelState(ModelState, nameof(StudentDto));

            return Page();
        }

        if (SelectedSchoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var students = await _studentRepository.GetAllAsync(s => s.SchoolId == SelectedSchoolId);

        if (students.Any(
            s => s.FirstName == studentDto.FirstName
            && s.LastName == studentDto.LastName
            && s.Age == studentDto.Age))
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
