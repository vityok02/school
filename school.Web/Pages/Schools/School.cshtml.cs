using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class CurrentSchoolModel : PageModel
{
    private readonly IRepository<School> _schoolRepository;
    private readonly IRepository<Address> _addressRepository;
    public School? School { get; set; }
    public Address? Address { get; set; }
    public CurrentSchoolModel(IRepository<School> schoolRepository, IRepository<Address> addressRepository)
    {
        _schoolRepository = schoolRepository;
        _addressRepository = addressRepository;
    }

    public IActionResult OnGet(int id)
    {
        Response.Cookies.Append("SchoolId", id.ToString());

        School = _schoolRepository.Get(id);
        if (School is null)
        {
            return NotFound("School not found");
        }

        Address = _addressRepository.Get(School?.Id ?? 0);
        return Page();
    }
}
