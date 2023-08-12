using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Positions;

public class AllPositionsModel : BasePageModel
{
    private readonly IPositionRepository _positionRepository;

    public IEnumerable<PositionDto> PositionDtos { get; private set; } = null!;
    public PaginatedList<PositionDto> Items { get; private set; } = default!;
    public string Filter { get; private set; } = null!;
    public string NameSort { get; private set; } = null!;
    public bool HasPositions => Items.Any();

    public AllPositionsModel(ISchoolRepository schoolRepository, IPositionRepository positionRepository)
        :base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task OnGetAsync(string orderBy, string filter, int? pageIndex)
    {
        NameSort = String.IsNullOrEmpty(orderBy) ? "name_desc" : "";

        Filter = filter;

        var positions = await _positionRepository.GetAllAsync(FilterBy(filter), Sort(orderBy));
        PositionDtos = positions.Select(p => p.ToPositionDto()).ToArray();

        Items = PaginatedList<PositionDto>.Create(PositionDtos, PageIndex = pageIndex ?? 1);
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
