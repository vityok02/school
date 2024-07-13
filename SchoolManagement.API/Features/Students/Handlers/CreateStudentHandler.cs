using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Students;
using SchoolManagement.API.Features.Students.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Students.Handlers;

public static class CreateStudentHandler
{
    public static async Task<IResult> Handle(
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

        var employees = await repository.GetAllAsync(e => e.SchoolId == schoolId);

        if (employees.Any(
            e => e.FirstName == studentDto.FirstName
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
        return Results.Created($"/schools/{schoolId}/students/{student.Id}", createdStudent);
    }
}
