using SchoolManagement.API.Employees;
using SchoolManagement.API.Floors;
using SchoolManagement.API.Positions;
using SchoolManagement.API.Rooms;
using SchoolManagement.API.Schools;
using SchoolManagement.API.Students;
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
