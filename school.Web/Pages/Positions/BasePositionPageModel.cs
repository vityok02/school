using FluentValidation;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Web.Pages.Positions
{
    public class BasePositionPageModel : BasePageModel
    {
        protected readonly IPositionRepository _positionRepository;
        protected readonly IValidator<PositionDto> _validator;

        public BasePositionPageModel(
            ISchoolRepository schoolRepository,
            IPositionRepository positionRepository,
            IValidator<PositionDto> validator)
            : base(schoolRepository)
        {
            _positionRepository = positionRepository;
            _validator = validator;
        }
    }
}
