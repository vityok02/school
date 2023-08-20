using SchoolManagement.API.Endpoints;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

SchoolEndpoints.Map(app);

FloorEndpoints.Map(app);

RoomEndpoints.Map(app);

app.MapGet("/schools/{id}/employees", async (IEmployeeRepository repository, int id) =>
    await repository.GetAllAsync(e => e.SchoolId == id));

app.MapGet("/schools/{id}/positions", async (IPositionRepository repository, int id) =>
    await repository.GetSchoolPositions(id));

app.MapGet("/schools/{id}/students", async (IRepository<Student> repository, int id) =>
    await repository.GetAllAsync(s => s.SchoolId == id));

app.Run();
