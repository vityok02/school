using SchoolManagement.Models.Interfaces;
using SchoolManagement.Models;
using FluentValidation;

namespace SchoolManagement.Web.Pages.Students;

public class BaseStudentPageModel : BasePageModel
{
    protected readonly IRepository<Student> _studentRepository;
    protected readonly IValidator<IStudentDto> _validator;

    public BaseStudentPageModel(
        ISchoolRepository schoolRepository,
        IRepository<Student> studentRepository, 
        IValidator<IStudentDto> validator)
         : base(schoolRepository)
    {
        _studentRepository = studentRepository;
        _validator = validator;
    }

}
