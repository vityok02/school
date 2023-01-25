using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class EditModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public Student? Student { get; set; }

    public EditModel(IRepository<School> schoolRepository, IRepository<Student> studentRepository)
        :base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnGet(int id)
    {
        Student = _studentRepository.Get(id);
        return Student is null ? RedirectToPage("List") : Page();
    }

    public IActionResult OnPost(int id, string firstName, string lastName, int age, string group)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var students = _studentRepository.GetAll(s => s.SchoolId == schoolId);
        if (students.Where(s => s.FirstName == firstName
            && s.LastName == lastName
            && s.Age == age).Count() > 1)
        {
            ErrorMessage = "Such student already exists";
        }

        var student = _studentRepository.Get(id);

        student!.UpdateInfo(firstName, lastName, age, group);

        _studentRepository.Update(student);
        return RedirectToPage("List");
    }
}