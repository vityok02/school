using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Positions;

public class PositionsListModel : BaseListPageModel
{
    private readonly IPositionRepository _positionRepository;

    public IEnumerable<PositionDto> PositionDtos { get; private set; } = null!;
    public string Filter { get; private set; } = null!;
    public string NameSort { get; private set; } = null!;
    public bool HasPositions => Items.Any();
    public override string ListPageUrl => "/Positions/List";

    public PositionsListModel(ISchoolRepository schoolRepository, IPositionRepository positionRepository)
        : base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task OnGetAsync(string orderBy, string filter, int? pageIndex)
    {
        NameSort = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";

        Filter = filter;

        var positions = await _positionRepository.GetAllAsync(FilterBy(filter), Sort(orderBy));
        PositionDtos = positions.Select(p => p.ToPositionDto()).ToArray();

        Items = new PaginatedList<object>(PositionDtos.Cast<object>(), PageIndex = pageIndex ?? 1);
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
            return RedirectToPage("/Positions/List");
        }

        await _positionRepository.DeleteAsync(position);

        return RedirectToPage("/Positions/List");
    }
}
