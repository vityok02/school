using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class DetailsModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public Student? Student { get; private set; }

    public DetailsModel(ISchoolRepository schoolRepository, IRepository<Student> studentRepository)
        :base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnGet(int id)
    {
        Student = _studentRepository.Get(id);

        return Student is null ? RedirectToPage("List") : Page();
    }
}