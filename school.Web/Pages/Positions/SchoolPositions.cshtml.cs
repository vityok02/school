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
    public string Filter { get; set; } = null!;
    public string NameSort { get; private set; } = null!;

    public SchoolPositionsModel(ISchoolRepository schoolRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<IActionResult> OnGetAsync(string orderBy, string filter)
    {
        NameSort = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";

        Filter = filter;

        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            RedirectToSchoolList();
        }

        var school = await SchoolRepository.GetAsync(schoolId);

        if (school is null)
        {
            return RedirectToSchoolList();
        }

        var allPositions = await _positionRepository.GetUnSelectedPositionsAsync(schoolId);
        AllPositions = allPositions.Select(s => s.ToPositionDto()).ToArray();
        var schoolPositions = await _positionRepository.GetSchoolPositionsAsync(schoolId);
        SchoolPositions = schoolPositions.Select(s => s.ToPositionDto()).ToArray();

        return Page();
    }

    public Expression<Func<Position, bool>> FilterBy(string filter)
    {
        return p => string.IsNullOrEmpty(filter) || p.Name.Contains(filter);
    }

    private Func<IQueryable<Position>, IOrderedQueryable<Position>> Sort(string orderBy)
    {
        if (orderBy == "name_desc")
        {
            return p => p.OrderByDescending(p => p.Name);
        }
        return p => p.OrderBy(p => p.Name);
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            RedirectToSchoolList();
        }

        var school = await SchoolRepository.GetAsync(schoolId);
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
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            RedirectToSchoolList();
        }

        var school = await SchoolRepository.GetAsync(schoolId);

        if (school is null)
        {
            RedirectToSchoolList();
        }

        var position = await _positionRepository.GetPositionAsync(id);

        if (position.Schools.Any(p => p.Id == schoolId))
        {
            RedirectToPage("SchoolPositions");
        }

        position!.Schools.Add(school!);

        await _positionRepository.UpdateAsync(position);

        return RedirectToPage("SchoolPositions");
    }
}