using FluentValidation;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.Web.Pages.Positions;

namespace SchoolManagement.Web.Pages.Employees
{
    public class BaseEmployeeModel : BasePageModel
    {
        protected IEmployeeRepository _employeeRepository;
        protected IPositionRepository _positionRepository;
        protected IValidator<IEmployeeDto> _validator;

        public IEnumerable<PositionDto> PositionDtos { get; set; } = default!;
        public string NotSelectedMessage { get; set; } = "";
        protected BaseEmployeeModel(
            ISchoolRepository schoolRepository,
            IEmployeeRepository employeeRepository,
            IPositionRepository positionRepository,
            IValidator<IEmployeeDto> validator) : base(schoolRepository)
        {
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
            _validator = validator;
        }
    }
}
