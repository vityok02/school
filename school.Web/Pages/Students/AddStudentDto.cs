namespace SchoolManagement.Web.Pages.Students;

public record AddStudentDto(string FirstName, string LastName, int Age, string Group)
    : IStudentDto;
