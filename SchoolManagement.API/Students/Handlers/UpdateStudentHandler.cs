using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Students.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Students.Handlers;

public static class UpdateStudentHandler
{
    public static async Task<IResult> Handle(
        IRepository<Student> repository,
        [FromRoute] int schoolId,
        [FromRoute] int studentId,
        [FromBody] StudentDto studentDto)
    {
        var student = (
            await repository
            .GetAllAsync(s => s.Id == studentId && s.SchoolId == schoolId))
            .SingleOrDefault();

        if (student is null) 
        {
            return Results.NotFound("No such student found");
        }

        student.UpdateInfo(studentDto.FirstName, studentDto.LastName, studentDto.Age, studentDto.Group);

        await repository.UpdateAsync(student);
        return Results.Ok(student);
    }
}
