using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages
{
    public class CurrentSchool
    {
        public IRepository<School> _schoolRepository;
        public IEnumerable<School> Schools { get; set; }

        public CurrentSchool(IRepository<School> schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public IEnumerable<School> GetSchools()
        {
            return _schoolRepository.GetAll();
        }
    }
}
