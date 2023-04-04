using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolModel : BasePageModel
{
    private readonly IRepository<Address> _addressRepository;

    public School? School { get; private set; }
    public Address? Address { get; private set; }

    public SchoolModel(ISchoolRepository schoolRepository, IRepository<Address> addressRepository)
        :base(schoolRepository)
    {
        _addressRepository = addressRepository;
    }

    public IActionResult OnGet(int id)
    {
        School = SchoolRepository.Get(id);
        if (School is null)
        {
            return RedirectToPage("List");
        }

        Address = _addressRepository.Get(School?.Id ?? 0);

        SelectedSchoolName = School!.Name;

        return Page();
    }
}
