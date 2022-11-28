using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class CurrentSchoolModel : PageModel
{
    private readonly IRepository<School> _schoolRepository;
    public IEnumerable<School> Schools { get; private set; }
    public string Message { get; private set; } = "";
    public int Id { get; set; }
    public School CurrentSchool { get; set; }
    public Address Address { get; set; }
    public CurrentSchoolModel(IRepository<School> schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public void OnGet(int id)
    {
        //CurrentSchool = Schools.Where(s => s.Id == id).SingleOrDefault();
        CurrentSchool = _schoolRepository.GetAll().Where(s => s.Id == id).SingleOrDefault();
        //Address = CurrentSchool.Address;
    }
}
