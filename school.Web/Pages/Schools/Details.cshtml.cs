using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolModel : PageModel
{
    private readonly IRepository<School> _schoolRepository;
    private readonly IRepository<Address> _addressRepository;

    public School? School { get; private set; }
    public Address? Address { get; private set; }

    public SchoolModel(IRepository<School> schoolRepository, IRepository<Address> addressRepository)
    {
        _schoolRepository = schoolRepository;
        _addressRepository = addressRepository;
    }

    public IActionResult OnGet(int id)
    {
        School = _schoolRepository.Get(id);
        if (School is null)
        {
            return RedirectToPage("List");
        }

        Response.Cookies.Append("SchoolId", id.ToString());

        Address = _addressRepository.Get(School?.Id ?? 0);

        return Page();
    }
}
