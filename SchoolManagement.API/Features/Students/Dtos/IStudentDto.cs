namespace SchoolManagement.API.Features.Students.Dtos;

public interface IStudentDto
{
    public string FirstName { get; }
    public string LastName { get; }
    public int Age { get; }
    public string Group { get; }
}
