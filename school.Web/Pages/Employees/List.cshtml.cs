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
        public string Message { get; set; } = "";
        public ListModel(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult OnGet()
        {
            var sId = HttpContext.Request.Cookies["SchoolId"];
            if (!int.TryParse(sId, out int schoolId) || sId is null)
            {
                return NotFound("School id not found");
            }

            Employees = _employeeRepository.GetAll().Where(e => e.SchoolId == schoolId);

            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee is null)
            {
                return NotFound("Employee not found");
            }

            _employeeRepository.Delete(employee!);

            return RedirectToPage("List");
        }
    }
}
