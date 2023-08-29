using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Students.Handlers;

public static class DeleteStudentHandler
{
    public static async Task<IResult> Handle(
        IRepository<Student> repository,
        [FromRoute] int schoolId,
        [FromRoute] int studentId)
    {
        var student = (
            await repository.GetAllAsync(s => s.Id == studentId && s.SchoolId == schoolId))
            .SingleOrDefault();

        if (student is null)
        {
            return Results.NotFound("No such student found");
        }

        await repository.DeleteAsync(student);
        return Results.Ok();
    }
}
