using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IRepository<Employee> _employeeRepository;
        public Employee? Employee { get; set; }
        public EditModel(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void OnGet(int id)
        {
            Employee = _employeeRepository.Get(id)!;
        }

        public IActionResult OnPost(int id, string firstName, string lastName, int age)
        {
            var employee = _employeeRepository.Get(id);
            if (employee == null) 
            {
                return NotFound("Employee is not found");
            }

            employee.UpdateInfo(firstName, lastName, age);

            _employeeRepository.Update(employee);
            return Redirect($"/employees/{id}");
        }
    }
}
