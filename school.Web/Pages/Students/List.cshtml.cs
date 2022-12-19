using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class ListModel : PageModel
{
    private readonly IRepository<Student> _studentRepository;
    public IEnumerable<Student>? Students { get; set; }
    public ListModel(IRepository<Student> studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnGet()
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        if (!int.TryParse(sId, out int schoolId))
        {
            return NotFound("School not found");
        }
        Students = _studentRepository.GetAll(s => s.SchoolId == schoolId);
        return Page();
    }

    public IActionResult OnPostDelete(int id)
    {
        var student = _studentRepository.Get(id);

        if (student is null)
        {
            return NotFound("Student not found");
        }

        _studentRepository.Delete(student);
        return RedirectToPage("List");
    }
}
