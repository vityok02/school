using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Students.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Students.Handlers;

public static class GetAllStudentsHandler
{
    public static async Task<IResult> Handle(
        IStudentRepository repository, 
        [FromRoute] int schoolId,
        [FromQuery] string? searchTerm,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page,
        int pageSize)
    {
        var studentsQuery = repository
            .GetStudentsQuery(schoolId, searchTerm, sortColumn, sortOrder)
            .Select(p => p.ToStudentDto());

        var students = await PagedList<StudentDto>.CreateAsync(studentsQuery, page, pageSize);

        return Results.Ok(students);
    }
}
