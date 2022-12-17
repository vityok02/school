using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using SchoolManagement.Data;

namespace SchoolManagement.Web.Pages.Schools
{
    public class EditModel : PageModel
    {
        private readonly IRepository<School> _schoolRepository;
        private readonly IRepository<Address> _addressRepository;
        [BindProperty]
        public School? School { get; set; }
        [BindProperty]
        public Address? Address { get; set; }
        public EditModel(IRepository<School> schoolRepository, AppDbContext ctx, IRepository<Address> addressRepository)
        {
            _schoolRepository = schoolRepository;
            _addressRepository = addressRepository;
        }
        public IActionResult OnGet(int id)
        {
            School = _schoolRepository.Get(id);
            Address = _addressRepository.Get(School?.Id ?? 0);
            return Page();
        }
        public IActionResult OnPost()
        {
            _schoolRepository.Update(School!);
            _addressRepository.Update(Address!);
            return RedirectToPage("List");
        }
    }
}
