using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees
{
    public class ListModel : PageModel
    {
        private readonly IRepository<Employee> _employeeRepository;
        public static IEnumerable<Employee>? Employees { get; set; }
        public ListModel(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult OnGet()
        {
            if (!int.TryParse(HttpContext.Request.Cookies["SchoolId"], out int schoolId))
            {
                return NotFound("School id is not found");
            }
            Employees = _employeeRepository.GetAll().Where(e => e.SchoolId == schoolId);
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            var employee = _employeeRepository.Get(id);
            _employeeRepository.Delete(employee!);

            return RedirectToPage("List");
        }
    }
}
