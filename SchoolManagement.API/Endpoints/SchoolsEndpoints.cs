using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Endpoints;

public static class SchoolsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools", async (ISchoolRepository repository) =>
            await repository.GetAllAsync());

        app.MapGet("/schools/{id}", async (ISchoolRepository repository, int id) => 
            await repository.GetSchoolAsync(id));

        //app.MapPost("/schools", null!);

        //app.MapPut("/schools/{id}", null!);

        //app.MapDelete("/schools/{id}", null!);
    }
}
