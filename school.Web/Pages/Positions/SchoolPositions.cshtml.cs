using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Positions;

public class SchoolPositionsModel : BasePageModel
{
    private readonly IPositionRepository _positionRepository;

    public IEnumerable<PositionDto> AllPositions { get; set; } = null!;
    public IEnumerable<PositionDto> SchoolPositions { get; set; } = null!;
    public string AllPositionsFilter { get; set; } = null!;
    public string SchoolPositionsFilter { get; set; } = null!;
    public string AllPositionsSort { get; private set; } = null!;
    public string SchoolPositionsSort { get; private set; } = null!;

    public SchoolPositionsModel(ISchoolRepository schoolRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<IActionResult> OnGetAsync(string schoolPositionsOrderBy, string allPositionsOrderBy, string allPositionsFilter, string schoolPositionsFilter)
    {
        AllPositionsSort = string.IsNullOrEmpty(allPositionsOrderBy) ? "name_desc" : "";
        SchoolPositionsSort = string.IsNullOrEmpty(schoolPositionsOrderBy) ? "name_desc" : "";

        AllPositionsFilter = allPositionsFilter;
        SchoolPositionsFilter = schoolPositionsFilter;

        if (!await HasSelectedSchool())
        {
            return RedirectToSchoolList();
        }

        var allPositions = await _positionRepository.GetAllPositions(SelectedSchoolId, FilterBy(this.AllPositionsFilter),
            Sort(allPositionsOrderBy));
        AllPositions = allPositions.Select(s => s.ToPositionDto()).ToArray();

        var schoolPositions = await _positionRepository.GetSchoolPositionsAsync(SelectedSchoolId, FilterBy(SchoolPositionsFilter),
            Sort(schoolPositionsOrderBy));
        SchoolPositions = schoolPositions.Select(s => s.ToPositionDto()).ToArray();

        return Page();
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        if (SelectedSchoolId == -1)
        {
            RedirectToSchoolList();
        }

        var school = await SchoolRepository.GetAsync(SelectedSchoolId);
        if (school is null)
        {
            RedirectToSchoolList();
        }

        var position = await _positionRepository.GetPositionAsync(id);

        if (position is null)
        {
            return RedirectToPage("SchoolPositions");
        }

        position.Schools.Remove(school!);
        position.Employees.Clear();

        await _positionRepository.SaveChangesAsync();

        return RedirectToPage("SchoolPositions");
    }

    public async Task<IActionResult> OnPostAdd(int id)
    {
        if (SelectedSchoolId == -1)
        {
            RedirectToSchoolList();
        }

        var school = await SchoolRepository.GetAsync(SelectedSchoolId);

        if (school is null)
        {
            RedirectToSchoolList();
        }

        var position = await _positionRepository.GetPositionAsync(id);

        if (position.Schools.Any(p => p.Id == SelectedSchoolId))
        {
            RedirectToPage("SchoolPositions");
        }

        position!.Schools.Add(school!);

        await _positionRepository.UpdateAsync(position);

        return RedirectToPage("SchoolPositions");
    }

    private static Func<IQueryable<Position>, IOrderedQueryable<Position>> Sort(string orderBy)
    {
        if (orderBy == "name_desc")
        {
            return p => p.OrderByDescending(p => p.Name);
        }
        return p => p.OrderBy(p => p.Name);
    }

    private Expression<Func<Position, bool>> FilterBy(string filter)
    {
        return p => string.IsNullOrEmpty(filter) || p.Name.Contains(filter);
    }
}