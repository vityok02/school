using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolListModel : BasePageModel
{
    private readonly IRepository<Address> _addressRepository;

    public IEnumerable<Address> Addresses { get; set; } = null!;
    public bool IsError { get; set; } = false;
    public bool IsFirst { get; set; } = false;

    public SchoolListModel(IRepository<School> schoolRepository, IRepository<Address> addressRepository)
        :base(schoolRepository)
    {
        _addressRepository = addressRepository;
    }

    public void OnGet(/*string sortOrder*/)
    {
        //NameSort = String.IsNullOrEmpty(sortOrder) ? "name-desc" : "";
        //DateSort = sortOrder == "Date" ? "date=desc" : "Date";

        Schools = GetSchools();

        //IQueryable<School> schoolsIQ = ;

        Addresses = _addressRepository.GetAll();
    }

    public void OnGetFirstTime()
    {
        IsFirst = true;
        OnGet();
    }

    public void OnGetError()
    {
        OnGet();
        IsError = true;
    }

    public IActionResult OnPostDelete(int id)
    {
        var school = SchoolRepository.Get(id);
        if (school is null)
        {
            return RedirectToPage("List");
        }
        
        SchoolRepository.Delete(school);
        return RedirectToPage("List");
    }
}
