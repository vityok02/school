using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ISchoolRepository schoolRepository, ILogger<IndexModel> logger)
            : base(schoolRepository)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("Schools/List");
        }
    }
}