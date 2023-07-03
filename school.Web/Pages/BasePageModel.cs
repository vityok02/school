using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages;

public abstract class BasePageModel : PageModel
{
    protected ISchoolRepository SchoolRepository { get; }

    public int SelectedSchoolId { get; set; }
    public string SelectedSchoolName { get; set; } = null!;
    public IEnumerable<School> Schools { get; set; } = null!;
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

    public override async Task OnPageHandlerExecutionAsync(
        PageHandlerExecutingContext context,
        PageHandlerExecutionDelegate next)
    {
        Schools = await GetSchoolsAsync();
        SelectedSchoolName = await GetSelectedSchoolNameAsync();
        SelectedSchoolId = GetSchoolId();

        await next.Invoke();
    }

    private int GetSchoolId()
    {
        var sId = HttpContext.Request.Cookies["SchoolId"];
        return int.TryParse(sId, out int schoolId) ? schoolId : -1;
    }

    private async Task<IEnumerable<School>> GetSchoolsAsync()
    {
        var schools = await SchoolRepository.GetAllAsync();
        return schools.OrderBy(s => s.Name);
    }

    private async Task<string> GetSelectedSchoolNameAsync()
    {
        var sId = GetSchoolId();

        SetSchoolId(sId);

        var school = await SchoolRepository.GetAsync(sId);
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

    public bool HasSelectedSchool()
    {
        bool result = true;

        if (SelectedSchoolId <= 0)
        {
            result = false;
        }

        var school = SchoolRepository.GetAsync(SelectedSchoolId);
        if (school is null)
        {
            result = false;
        }

        return result;
    }
}
