using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class StudentFormModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public StudentFormModel(IRepository<School> schoolRepository, IRepository<Student> studentRepository)
        :base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnPost(string firstName, string lastName, int age, string group)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        var students = _studentRepository
            .GetAll(s => s.SchoolId == schoolId 
            && s.FirstName == firstName
            && s.LastName == lastName
            && s.Age == age);

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

        Student student = new(firstName, lastName, age, group)
        {
            School = school!
        };

        _studentRepository.Add(student);
        return RedirectToPage("List");
    }
}
