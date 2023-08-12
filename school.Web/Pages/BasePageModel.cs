using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages;

public abstract class BasePageModel : PageModel
{
    protected ISchoolRepository SchoolRepository { get; }

    public int SelectedSchoolId { get; protected set; }
    public string SelectedSchoolName { get; protected set; } = null!;
    public IEnumerable<School> Schools { get; private set; } = null!;
    public string ErrorMessage { get; protected set; } = null!;
    public string Message { get; protected set; } = null!;
    public string OrderBy { get; protected set; } = null!;
    public string FirstNameSort { get; protected set; } = null!;
    public string LastNameSort { get; protected set; } = null!;
    public string AgeSort { get; protected set; } = null!;
    public string FilterByName { get; protected set; } = null!;
    public int FilterByAge { get; protected set; }
    public IDictionary<string, string> FilterParams { get; protected set; } = null!;
    public IDictionary<string, string> FirstNameParams { get; protected set; } = null!;
    public IDictionary<string, string> LastNameParams { get; protected set; } = null!;
    public IDictionary<string, string> AgeParams { get; protected set; } = null!;
    public int PageIndex { get; protected set; }

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
        return RedirectToPage("/Schools/List");
    }

    public void SelectSchool(int selectedSchoolId)
    {
        SetSchoolId(selectedSchoolId);
    }

    public async Task<bool> HasSelectedSchoolAsync()
    {
        bool result = true;

        if (SelectedSchoolId <= 0)
        {
            result = false;
        }

        var school = await SchoolRepository.GetAsync(SelectedSchoolId);
        if (school is null)
        {
            result = false;
        }

        return result;
    }
}
