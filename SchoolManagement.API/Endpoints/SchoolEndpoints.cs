using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Endpoints;

public static class SchoolEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools", async (ISchoolRepository repository) =>
            await repository.GetAllAsync());

        app.MapPost("/schools", ());

        app.MapGet("/schools/{id}", ());

        app.MapPut("/schools/{id}", ());

        app.MapDelete("/schools/{id}", ());
    }
}
