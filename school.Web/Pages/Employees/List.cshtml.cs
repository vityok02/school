using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public void OnGet()
        {
            var schoolId = int.Parse(HttpContext.Request.Cookies["SchoolId"]!);
            Employees = _employeeRepository.GetAll().Where(e => e.SchoolId == schoolId);
        }
    }
}
