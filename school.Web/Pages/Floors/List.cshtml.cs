using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Floors;

public class FloorListModel : PageModel
{
    AppDbContext _ctx;
    private readonly IRepository<Floor>? _floorRepository;
    private readonly IRepository<School> _schoolRepository;

    public FloorListModel(IRepository<Floor>? floorRepository, IRepository<School> schoolRepository, AppDbContext ctx)
    {
        _floorRepository = floorRepository;
        _schoolRepository = schoolRepository;
        _ctx = ctx;
        Floors = _ctx.Floors.Where(f=>f.SchoolId == SchoolId);
    }
    public School School { get; set; }
    public IEnumerable<School> Schools { get; set; }
    public static IEnumerable<Floor> Floors { get; private set; }

    public int SchoolId { get; set; }


    public void OnGet()
    {
        Floors = _floorRepository.GetAll(f => f.SchoolId == SchoolId);
        SchoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]);
    }

    public void OnPost()
    {

    }

    public void OnPostAddFloor()
    {
        SchoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]);
        School = _schoolRepository.Get(SchoolId);
        int number;

        if (!Floors.Any())
            number = 0;
        else
            number = Floors.Last().Number;

        Floor floor = new(number + 1);
        //_floorRepository!.Add(floor); ?
        var (valid, error) = School.AddFloor(floor);
        _ctx.SaveChanges();
    }
    public void OnPostAddBasement()
    {
        SchoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]);
        School = _schoolRepository.Get(SchoolId);
        int number;
        if (!Floors.Any())
            number = 0;
        else
            number = Floors.First().Number;

        Floor floor = new(number - 1);
        var (valid, error) = School!.AddFloor(floor);
        _ctx.SaveChanges();
    }
    //public IActionResult OnPostDelete(int id)
    //{
    //    var floor = _floorRepository.Get(id);
    //    //_ctx.Remove(floor);
    //    //_ctx.SaveChanges();
    //    _floorRepository.Delete(floor!);
    //    return RedirectToPage("List");
    //}
}
