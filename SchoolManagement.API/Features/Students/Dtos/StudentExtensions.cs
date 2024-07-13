using SchoolManagement.Models;

namespace SchoolManagement.API.Features.Students.Dtos;

public static class StudentExtensions
{
    public static StudentDto ToStudentDto(this Student student)
    {
        return new StudentDto(
            student.Id,
            student.FirstName,
            student.LastName,
            student.Age,
            student.Group);
    }
}
