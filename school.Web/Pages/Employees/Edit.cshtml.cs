using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IRepository<Employee> _employeeRepository;
        [BindProperty]
        public Employee Employee { get; set; }
        public EditModel(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void OnGet(int id)
        {
            Employee = _employeeRepository.Get(id)!;
        }

        public IActionResult OnPost()
        {
            _employeeRepository.Update(Employee);
            return RedirectToPage("Details");
        }
    }
}
