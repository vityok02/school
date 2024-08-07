﻿using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Students;
using SchoolManagement.API.Features.Students.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Students.Handlers;

public static class GetStudentByIdHandler
{
    public static async Task<IResult> Handle(
        IRepository<Student> repository,
        [FromRoute] int schoolId,
        [FromRoute] int studentId)
    {
        var student = (
            await repository
            .GetAllAsync(s => s.Id == studentId && s.SchoolId == schoolId))
            .SingleOrDefault();

        if (student is null)
        {
            return Results.NotFound(StudentErrorMessages.NotFound);
        }

        var studentDto = student.ToStudentDto();
        return Results.Ok(studentDto);
    }
}
