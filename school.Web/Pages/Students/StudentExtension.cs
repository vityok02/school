using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Students;

public static class StudentExtension
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
