using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Positions;

public class AllPositionsModel : BasePageModel
{
    private readonly IPositionRepository _positionRepository;

    public string Filter { get; set; } = null!;
    public string NameSort { get; private set; } = null!;
    public bool HasPositions => Positions?.Any() ?? false;
    public IEnumerable<Position> Positions { get; set; } = null!;

    public AllPositionsModel(ISchoolRepository schoolRepository, IPositionRepository positionRepository)
        :base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task OnGetAsync(string orderBy, string filter)
    {
        NameSort = String.IsNullOrEmpty(orderBy) ? "name_desc" : "";

        Filter = filter;

        Positions = await _positionRepository.GetAllAsync(FilterBy(filter), Sort(orderBy));
    }

    public Expression<Func<Position, bool>> FilterBy(string filter)
    {
        return p => (string.IsNullOrEmpty(filter) || p.Name.Contains(filter));
    }

    private static Func<IQueryable<Position>, IOrderedQueryable<Position>> Sort(string orderBy)
    {
        if (orderBy == "name_desc")
        {
            return p => p.OrderByDescending(p => p.Name);
        }
        return p => p.OrderBy(p => p.Name);
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var position = await _positionRepository.GetAsync(id);
        if (position is null)
        {
            return RedirectToPage("AllPositions");
        }

        await _positionRepository.DeleteAsync(position);

        return RedirectToPage("AllPositions");
    }
}
