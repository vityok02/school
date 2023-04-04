using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Positions;

public class SchoolPositionsModel : BasePageModel
{
    private readonly IPositionRepository _positionRepository;

    public IEnumerable<Position> SchoolPositions { get; set; } = null!;
    public string Filter { get; set; } = null!;
    public string NameSort { get; private set; } = null!;
    public IEnumerable<Position> AllPositions { get; set; } = null!;

    public SchoolPositionsModel(ISchoolRepository schoolRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public void OnGet(string orderBy, string filter)
    {
        NameSort = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";

        Filter = filter;

        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            RedirectToSchoolList();
        }

        var school = SchoolRepository.Get(schoolId);

        if (school is null)
        {
            RedirectToSchoolList();
        }

        AllPositions = _positionRepository.GetUnSelectedPositions(schoolId);
        SchoolPositions = _positionRepository.GetSchoolPositions(schoolId);
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

    public IActionResult OnPostDelete(int id)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            RedirectToSchoolList();
        }

        var school = SchoolRepository.Get(schoolId);
        if (school is null)
        {
            RedirectToSchoolList();
        }

        var position = _positionRepository.GetPosition(id);

        if (position is null)
        {
            return RedirectToPage("SchoolPositions");
        }

        position.Schools.Remove(school!);
        position.Employees.Clear();

        _positionRepository.SaveChanges();

        return RedirectToPage("SchoolPositions");
    }

    public IActionResult OnPostAdd(int id)
    {
        var schoolId = GetSchoolId();
        if (schoolId == -1)
        {
            RedirectToSchoolList();
        }

        var school = SchoolRepository.Get(schoolId);

        if (school is null)
        {
            RedirectToSchoolList();
        }

        var position = _positionRepository.GetPosition(id);

        if (position.Schools.Any(p => p.Id == schoolId))
        {
            RedirectToPage("SchoolPositions");
        }

        position!.Schools.Add(school!);

        _positionRepository.Update(position);

        return RedirectToPage("SchoolPositions");
    }
}