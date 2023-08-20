using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddMvc();

builder.Services.AddValidatorsFromAssemblyContaining<ValidatorMarker>();

// Add services to the container.
builder.Services.AddRazorPages()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = true;
    });
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
