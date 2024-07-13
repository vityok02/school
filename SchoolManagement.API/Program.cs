using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.API;
using SchoolManagement.API.Employees;
using SchoolManagement.API.Floors;
using SchoolManagement.API.Positions;
using SchoolManagement.API.Rooms;
using SchoolManagement.API.Schools;
using SchoolManagement.API.Students;
using SchoolManagement.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<ValidatorMarker>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "/swagger";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    using var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
    context.Database.Migrate();
    await DataSeeder.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Database was not created");
}

EmployeesEndpoints.Map(app);
SchoolsEndpoints.Map(app);
FloorsEndpoints.Map(app);
RoomsEndpoints.Map(app);
PositionsEndpoints.Map(app);
SchoolPositionsEndpoints.Map(app);
StudentsEndpoints.Map(app);

app.Run();
