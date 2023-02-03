using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages;

public abstract class BasePageModel : PageModel
{
    protected IRepository<School> SchoolRepository { get; }

    public string SelectedSchoolName { get; set; } = null!;
    public IEnumerable<School> Schools { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;
    public string Message { get; set; } = null!;

    public string FilterByName { get; set; } = null!;
    public int FilterByAge { get; set; }

    protected BasePageModel(IRepository<School> schoolRepository)
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

    public IActionResult SelectSchool(int selectedSchool)
    {
        SetSchoolId(selectedSchool);

        return Redirect("/Schools/" + selectedSchool);
    }
}
