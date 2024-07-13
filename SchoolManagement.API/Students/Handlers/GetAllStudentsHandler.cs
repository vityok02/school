using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Students.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Students.Handlers;

public static class GetAllStudentsHandler
{
    public static async Task<IResult> Handle(IRepository<Student> repository, [FromRoute] int schoolId)
    {
        var students = await repository.GetAllAsync(s => s.SchoolId == schoolId);

        var studentItemDtos = students.Select(s => s.ToStudentDto());
        return Results.Ok(studentItemDtos);
    }
}
