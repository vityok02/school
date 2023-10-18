using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Students.Dtos;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Students.Handlers;

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

        if (employees.Any(
            e => e.FirstName == studentDto.FirstName
            && e.LastName == studentDto.LastName
            && e.Age == studentDto.Age))
        {
            return Results.BadRequest("Such student already exists");
        }

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

        var updatedStudentDto = student.ToStudentDto();
        return Results.Ok(updatedStudentDto);
    }
}
