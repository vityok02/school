using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Employee> _employeeRepository;
        public Employee? Employee { get; set; }
        public DetailsModel(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void OnGet(int id)
        {
            Employee = _employeeRepository.Get(id);
        }
    }
}