using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagement.Web.Pages.Positions;

public class AllPositionsModel : BasePageModel
{
    private readonly IRepository<Position> _positionRepository;

    public string Filter { get; set; } = null!;
    public string NameSort { get; private set; } = null!;
    public IEnumerable<Position> Positions { get; set; } = null!;

    public AllPositionsModel(IRepository<School> schoolRepository, IRepository<Position> positionRepository)
        :base(schoolRepository)
    {
        _positionRepository = positionRepository;
    }

    public void OnGet(string orderBy, string filter)
    {
        NameSort = String.IsNullOrEmpty(orderBy) ? "name_desc" : "";

        Filter = filter;

        Positions = _positionRepository.GetAll(FilterBy(filter), Sort(orderBy));
    }

    public Expression<Func<Position, bool>> FilterBy(string filter)
    {
        return p => (string.IsNullOrEmpty(filter) || p.Name.Contains(filter));
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
        var position = _positionRepository.Get(id);
        if (position is null)
        {
            return RedirectToPage("AllPositions");
        }

        _positionRepository.Delete(position);

        return RedirectToPage("AllPositions");
    }
}