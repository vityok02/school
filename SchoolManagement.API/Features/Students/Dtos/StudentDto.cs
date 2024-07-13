namespace SchoolManagement.API.Features.Students.Dtos;

public record StudentDto(
    int Id,
    string FirstName,
    string LastName,
    int Age,
    string Group)
    : IStudentDto;
