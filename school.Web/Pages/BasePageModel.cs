using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages;

public abstract class BasePageModel : PageModel
{
    protected IRepository<School> SchoolRepository { get; }

    public IEnumerable<School> Schools { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;

    protected BasePageModel(IRepository<School> schoolRepository)
    {
        SchoolRepository = schoolRepository;
    }

    protected int GetSchoolId()
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        return int.TryParse(sId, out int schoolId) ? schoolId : -1;
    }

    protected void SetSchoolId(int schoolId)
    {
        Response.Cookies.Append("SchoolId", schoolId.ToString());
    }

    public IEnumerable<School> GetSchools() => SchoolRepository.GetAll();

    protected IActionResult RedirectToSchoolList()
    {
        return RedirectToPage("/SchoolsController/List", "error");
    }

    public IActionResult OnPostSelectSchool(int selectedSchool)
    {
        SetSchoolId(selectedSchool);

        return Redirect("/Schools/" + selectedSchool);
    }
}
