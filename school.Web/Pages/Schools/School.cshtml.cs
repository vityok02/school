using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class CurrentSchoolModel : PageModel
{
    private readonly IRepository<School> _schoolRepository;
    private readonly IRepository<Address> _addressRepository;
    public IEnumerable<School> Schools { get; private set; }
    public string Message { get; private set; } = "";
    public int Id { get; set; }
    public School CurrentSchool { get; set; }
    public Address Address { get; set; }
    public CurrentSchoolModel(IRepository<School> schoolRepository, IRepository<Address> addressRepository)
    {
        _schoolRepository = schoolRepository;
        _addressRepository = addressRepository;
    }

    public void OnGet(int id)
    {
        //CurrentSchool = Schools.Where(s => s.Id == id).SingleOrDefault();
        CurrentSchool = _schoolRepository.GetAll().Where(s => s.Id == id).SingleOrDefault();
        Address = _addressRepository.GetAll().Where(a => a.Id == CurrentSchool.Id).SingleOrDefault();
        //Address = CurrentSchool.Address;
    }
}
