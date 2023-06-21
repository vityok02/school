namespace SchoolManagement.Web.Pages.Students;

public interface IStudentDto
{
    string FirstName { get; }
    string LastName { get; }
    int Age { get; }
    string Group { get; }
}
