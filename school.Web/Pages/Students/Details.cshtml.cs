using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class DetailsModel : PageModel
{
    private readonly IRepository<Student> _studentRepository;

    public Student? Student { get; private set; }

    public DetailsModel(IRepository<Student> studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnGet(int id)
    {
        Student = _studentRepository.Get(id);

        return Student is null ? RedirectToPage("List") : Page();
    }
}