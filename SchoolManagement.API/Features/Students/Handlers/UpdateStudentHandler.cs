using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Students.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Students.Handlers;

public static class UpdateStudentHandler
{
    public static async Task<IResult> Handle(
        IValidator<IStudentDto> validator,
        IRepository<Student> repository,
        [FromRoute] int schoolId,
        [FromRoute] int studentId,
        [FromBody] StudentDto studentDto)
    {
        var validationResult = await validator.ValidateAsync(studentDto);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var employees = await repository.GetAllAsync(e => e.SchoolId == schoolId && e.Id != studentDto.Id);

        if (await repository.AnyAsync(
            e => e.SchoolId == schoolId
            && e.Id != studentDto.Id
            && e.FirstName == studentDto.FirstName
            && e.LastName == studentDto.LastName
            && e.Age == studentDto.Age))
        {
            return Results.Conflict(StudentErrorMessages.Dublicate);
        }

        var student = (
            await repository
            .GetAllAsync(s => s.Id == studentId && s.SchoolId == schoolId))
            .SingleOrDefault();

        if (student is null)
        {
            return Results.NotFound(StudentErrorMessages.NotFound);
        }

        student.UpdateInfo(studentDto.FirstName, studentDto.LastName, studentDto.Age, studentDto.Group);

        await repository.UpdateAsync(student);

        var updatedStudentDto = student.ToStudentDto();
        return Results.Ok(updatedStudentDto);
    }
}
