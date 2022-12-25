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
        public string Message { get; set; } = "";
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
        public IActionResult OnPost(string name)
        {
            var schools = _schoolRepository.GetAll();

            if (schools.Where(s => s.Name == name).Count() > 1)
            {
                Message = "School with this name already exists";
                return Page();
            }

            _schoolRepository.Update(School!);
            _addressRepository.Update(Address!);
            return RedirectToPage("List");
        }
    }
}
