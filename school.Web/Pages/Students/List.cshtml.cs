using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Students;

public class ListModel : BasePageModel
{
    private readonly IRepository<Student> _studentRepository;

    public IEnumerable<Student> Students { get; set; } = null!;
    public string FirstNameSort { get; set; } = null!;
    public string LastNameSort { get; set; } = null!;
    public string AgeSort { get; set; } = null!;
    public string CurrentFilter { get; set; }
    public string FilterByGroup { get; set; }

    public ListModel(IRepository<School> schoolRepository, IRepository<Student> studentRepository)
        : base(schoolRepository)
    {
        _studentRepository = studentRepository;
    }

    public IActionResult OnGet(string orderBy, string filterByName, int filterByAge, string filterByGroup)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            return RedirectToSchoolList();
        }

        FirstNameSort = String.IsNullOrEmpty(orderBy) ? "name_desc" : "";
        LastNameSort = orderBy == "lastName" ? "lastName_desc" : "lastName";
        AgeSort = orderBy == "age" ? "age_desc" : "age";

        IEnumerable<Student> studentsIq = _studentRepository.GetAll(s => s.SchoolId == schoolId);

        studentsIq = FilterBy(filterByName, filterByAge, filterByGroup);

        studentsIq = Sort(orderBy, studentsIq);

        IQueryable<Student> studentsQueryable = studentsIq.AsQueryable();

        Students = studentsIq;

        return Page();

        static IEnumerable<Student> Sort(string orderBy, IEnumerable<Student> studentsIq)
        {
            studentsIq = orderBy switch
            {
                "name_desc" => studentsIq.OrderByDescending(s => s.FirstName),
                "lastName" => studentsIq.OrderBy(s => s.LastName),
                "lastName_desc" => studentsIq.OrderByDescending(s => s.LastName),
                "age" => studentsIq.OrderBy(s => s.Age),
                "age_desc" => studentsIq.OrderByDescending(s => s.Age),
                "group" => studentsIq.OrderBy(s => s.Group),
                "group_desc" => studentsIq.OrderByDescending(s => s.Group),
                _ => studentsIq.OrderBy(s => s.FirstName),
            };
            return studentsIq;
        }

        IEnumerable<Student> FilterBy(string filterByName, int filterByAge, string filterByGroup)
        {
            if (!String.IsNullOrEmpty(filterByName))
            {
                studentsIq = studentsIq
                    .Where(s => s.FirstName
                    .ToUpper()
                    .Contains(filterByName.ToUpper()) 
                    || s.LastName
                    .ToUpper()
                    .Contains(filterByName.ToUpper()));
            }
            else if (filterByAge != 0)
            {
                studentsIq = studentsIq.Where(s => s.Age == filterByAge);
            }
            else if (!String.IsNullOrEmpty(filterByName))
            {
                studentsIq = studentsIq.Where(s => s.Group == filterByGroup);
            }
            else
            {
                Message = "No matching results";
            }
            return studentsIq;
        
        }
    }


    private IEnumerable<Student> FilterBy(string filterBy)
    {
        if (filterBy is not null)
        {
            Students = Students.Where(s => s.FirstName == filterBy
            || s.LastName == filterBy
            || s.Age.ToString() == filterBy
            || s.Group == filterBy);
        }
        else
        {
            ViewData["EmptyMessage"] = "No matching results";
        }
        return Students;
    }

    public IActionResult OnPostDelete(int id)
    {
        var student = _studentRepository.Get(id);
        if (student is null)
        {
            return RedirectToPage("List");
        }

        _studentRepository.Delete(student);
        return RedirectToPage("List");
    }
}
