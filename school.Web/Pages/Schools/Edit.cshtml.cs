using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

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
        public string Message { get; private set; } = "";

        public EditModel(IRepository<School> schoolRepository, IRepository<Address> addressRepository)
        {
            _schoolRepository = schoolRepository;
            _addressRepository = addressRepository;
        }

        public IActionResult OnGet(int id)
        {
            School = _schoolRepository.Get(id);
            if (School is null)
            {
                return NotFound("School not found");
            }

            Address = _addressRepository.Get(School?.Id ?? 0);
            return Page();
        }

        public IActionResult OnPost(Address address)
        {
            School!.Address = address;

            _schoolRepository.Update(School!);
            return RedirectToPage("List");
        }
    }
}
