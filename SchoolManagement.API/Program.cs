using SchoolManagement.API.Endpoints;
using SchoolManagement.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

SchoolsEndpoints.Map(app);

FloorsEndpoints.Map(app);

RoomsEndpoints.Map(app);

EmployeesEndpoints.Map(app);

PositionsEndpoints.Map(app);

StudentsEndpoints.Map(app);

app.Run();
