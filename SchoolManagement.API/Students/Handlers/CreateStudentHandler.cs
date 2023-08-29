using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Students.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Students.Handlers;

public static class CreateStudentHandler
{
    public static async Task<IResult> Handle(
        IRepository<Student> repository,
        [FromRoute] int schoolId,
        [FromBody] StudentCreateDto studentDto)
    {
        var student = new Student(
            studentDto.FirstName,
            studentDto.LastName,
            studentDto.Age,
            studentDto.Group)
        {
            SchoolId = schoolId
        };

        await repository.AddAsync(student);

        var createdStudent = student.ToStudentDto();
        return Results.Created($"/schools/{schoolId}/students/{student.Id}", createdStudent);
    }
}
