using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Positions
{
    public class ListModel : BasePageModel
    {
        private readonly IRepository<Position> _positionRepository;

        public IEnumerable<Position> Positions { get; set; } = null!;

        public ListModel(IRepository<School> schoolRepository, IRepository<Position> positionRepository)
            :base(schoolRepository)
        {
            _positionRepository = positionRepository;
        }

        public void OnGet()
        {
            Positions = _positionRepository.GetAll();
        }

        public IActionResult OnPostDelete(int id)
        {
            var position = _positionRepository.Get(id);
            if (position is null)
            {
                return RedirectToPage("List");
            }

            _positionRepository.Delete(position);

            return RedirectToPage("List");
        }
    }
}
