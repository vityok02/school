using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Data;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Web.Pages.Students;

public class StudentFormModel : PageModel
{
    AppDbContext _dbContext;
    public readonly IRepository<School> _schoolRepository;
    public IEnumerable<School> Schools { get; private set; }
    public string Message { get; set; } = "";
    public StudentFormModel(IRepository<School> schoolRepository, AppDbContext db)
    {
        _schoolRepository = schoolRepository;
        _dbContext = db;
    }
    public void OnGet(int id)
    {
        Schools = _schoolRepository.GetAll();
    }
    public IActionResult OnPost(int id, string firstName, string lastName, int age, string group)
    {
        var currentSchool = _dbContext.Schools
            .Where(s => s.Id == id)
            .Include(s => s.Students)
            .SingleOrDefault();

        var (valid, error) = currentSchool!.AddStudent(new Student(firstName, lastName, age, group));
        if (!valid)
        {
            Message = error!;
            return Page();
        }

        _dbContext.SaveChanges();
        return Redirect($"/school/{id}");
    }
}
