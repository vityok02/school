using FluentValidation;
using Microsoft.AspNetCore.Identity;
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

builder.Services.AddDefaultIdentity<IdentityUser<int>>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

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

EmployeesEndpoints.Map(app);
SchoolsEndpoints.Map(app);
FloorsEndpoints.Map(app);
RoomsEndpoints.Map(app);
PositionsEndpoints.Map(app);
SchoolPositionsEndpoints.Map(app);
StudentsEndpoints.Map(app);

app.Run();
