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

    public void OnGet(string orderBy)
    {
        Schools = SchoolRepository.GetAll(Sort(orderBy));

        Addresses = _addressRepository.GetAll();

        Func<IQueryable<School>, IOrderedQueryable<School>> Sort(string orderBy)
        {
            return orderBy switch
            {
                "name" => s => s.OrderBy(s => s.Name),
                "city" => s => s.OrderBy(s => s.Address.City),
                "street" => s => s.OrderBy(s => s.Address.Street),
                _ => s => s.OrderBy(s => s.Name),
            };
        }
    }


    public void OnGetFirstTime(string orderBy)
    {
        IsFirst = true;
        OnGet(orderBy);
    }

    public void OnGetError(string orderBy)
    {
        IsError = true;
        IsFirst = true;
        OnGet(orderBy);
    }

    public IActionResult OnGetSelectSchool(int id, string orderBy)
    {
        OnGet(orderBy);
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