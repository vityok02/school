using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Students.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Students.Handlers;

public static class CreateStudentHandler
{
    public static async Task<IResult> Handle(
        HttpContext context,
        LinkGenerator linkGenerator,
        IValidator<IStudentDto> validator,
        IRepository<Student> repository,
        [FromRoute] int schoolId,
        [FromBody] StudentCreateDto studentDto)
    {
        var validationResult = await validator.ValidateAsync(studentDto);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (await repository.AnyAsync(
            e => e.SchoolId == schoolId
            && e.FirstName == studentDto.FirstName
            && e.LastName == studentDto.LastName
            && e.Age == studentDto.Age))
        {
            return Results.Conflict(StudentErrorMessages.Dublicate);
        }

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

        var uri = linkGenerator.GetUriByName(context, "Get student by id", new
        {
            studentId = student.Id,
        });

        return Results.Created(uri, createdStudent);
    }
}
