using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Schools
{
    public class EditModel : BasePageModel
    {
        private readonly IRepository<Address> _addressRepository;

        [BindProperty]
        public School? School { get; set; }

        [BindProperty]
        public Address? Address { get; set; }

        public EditModel(ISchoolRepository schoolRepository, IRepository<Address> addressRepository)
            :base(schoolRepository)
        {
            _addressRepository = addressRepository;
        }

        public IActionResult OnGet(int id)
        {
            School = SchoolRepository.Get(id);
            if (School is null)
            {
                return RedirectToPage("List");
            }

            Address = _addressRepository.Get(School?.Id ?? 0);
            return Page();
        }

        public IActionResult OnPost(Address address, int id)
        {
            School!.Address = address;

            SchoolRepository.Update(School!);
            return Redirect($"/schools/{id}");
        }
    }
}
