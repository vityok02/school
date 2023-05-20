namespace SchoolManagement.Web.Pages.Students;

public record StudentDto(int Id, string FirstName, string LastName, int Age, string Group)
    : IStudentDto;
