using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IRepository<School> schoolRepository, ILogger<IndexModel> logger)
            : base(schoolRepository)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}