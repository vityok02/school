using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Dto;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages;

public abstract class BasePageModel : PageModel
{
    protected ISchoolRepository SchoolRepository { get; }

    public string SelectedSchoolName { get; set; } = null!;
    public IEnumerable<SchoolDto> Schools { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string OrderBy { get; set; } = null!;
    public string FirstNameSort { get; set; } = null!;
    public string LastNameSort { get; set; } = null!;
    public string AgeSort { get; set; } = null!;
    public string FilterByName { get; set; } = null!;
    public int FilterByAge { get; set; }
    public IDictionary<string, string> FilterParams { get; set; } = null!;
    public IDictionary<string, string> FirstNameParams { get; set; } = null!;
    public IDictionary<string, string> LastNameParams { get; set; } = null!;
    public IDictionary<string, string> AgeParams { get; set; } = null!;

    protected BasePageModel(ISchoolRepository schoolRepository)
    {
        SchoolRepository = schoolRepository;
    }

    protected int GetSchoolId()
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        return int.TryParse(sId, out int schoolId) ? schoolId : -1;
    }
    
    public IEnumerable<School> GetSchools() => SchoolRepository.GetAll().OrderBy(s => s.Name);

    public string GetSelectedSchoolName()
    {
        var sId = GetSchoolId();

        SetSchoolId(sId);

        var school = SchoolRepository.Get(sId);
        if(school is null)
        {
            return "Not selected";
        }

        return school!.Name;
    }

    protected void SetSchoolId(int schoolId)
    {
        Response.Cookies.Append("SchoolId", schoolId.ToString());
    }


    protected IActionResult RedirectToSchoolList()
    {
        return RedirectToPage("/Schools/List", "error");
    }

    public void SelectSchool(int selectedSchoolId)
    {
        SetSchoolId(selectedSchoolId);
    }
}
