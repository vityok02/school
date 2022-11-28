using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Data;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Web.Pages.Employees;

public class EmployeeFormModel : PageModel
{
    AppDbContext _dbContext;
    public readonly IRepository<School> _schoolRepository;
    public IEnumerable<School> Schools { get; private set; }
    public string Message { get; set; } = "";
    public EmployeeFormModel(IRepository<School> schoolRepository, AppDbContext db)
    {
        _schoolRepository = schoolRepository;
        _dbContext = db;
    }
    public void OnGet(int id)
    {
        Schools = _schoolRepository.GetAll();
    }
    public IActionResult OnPost(int id, string firstName, string lastName, int age, string type)
    {
        var currentSchool = _dbContext.Schools
            .Where(s => s.Id == id)
            .Include(s => s.Employees)
            .SingleOrDefault();

        Employee? employee = null;

        if (type == "T")
        {
            employee = new Teacher(firstName, lastName, age);
        }
        else if (type == "D")
        {
            employee = new Director(firstName, lastName, age);
        }
        else
        {
            Message = "Wrong employee type";
            return Page();
        }

        var (valid, error) = currentSchool!.AddEmployee(employee);
        if (!valid)
        {
            Message = error!;
            return Page();
        }

        _dbContext.SaveChanges();
        return Redirect($"/school/{id}");
    }
}
