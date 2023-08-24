using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Endpoints;

public static class StudentsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/students", async (IRepository<Student> repository, int id) =>
            await repository.GetAllAsync(s => s.SchoolId == id));

        app.MapGet("/schools/{id}/students/{studentId}", async (IRepository<Student> repository, int studentId) =>
            await repository.GetAsync(studentId));
    }
}
