using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools;

public class SchoolListModel : BasePageModel
{
    private readonly IRepository<Address> _addressRepository;
    public readonly IRepository<Employee> _empRepository;

    public IEnumerable<Address> Addresses { get; set; } = null!;
    public bool IsError { get; set; } = false;
    public bool IsFirst { get; set; } = false;

    public SchoolListModel(IRepository<School> schoolRepository, IRepository<Address> addressRepository, IRepository<Employee> empRepository)
        : base(schoolRepository)
    {
        _addressRepository = addressRepository;
        _empRepository = empRepository;
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
        IsError = true;
        IsFirst = true;
        OnGet();
    }

    public IActionResult OnGetSelectSchool(int id)
    {
        OnGet();
        var school = SchoolRepository.Get(id);
        if(school is null)
        {
            IsError = true;
        }

        SelectedSchoolName = school.Name;

        SetSchoolId(school!.Id);
        return Page();
    }

    public IActionResult OnPostDelete(int id)
    {
        var school = SchoolRepository.Get(id);
        if (school is null)
        {
            return RedirectToPage("List");
        }

        var employees = _empRepository.GetAll(e => e.SchoolId == id);
        foreach (var emp in employees)
        {
            school.Employees.Clear();
        }

        SchoolRepository.Delete(school);
        return RedirectToPage("List");
    }
}