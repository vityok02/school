using SchoolManagement.Client.Components;
using SchoolManagement.Client.Constants;
using SchoolManagement.Client.Features.Schools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient(ApiName.Value, c => c.BaseAddress = new Uri("https://localhost:7128"));

builder.Services.AddScoped<ISchoolService, SchoolService>();
//builder.Services.AddValidatorsFromAssemblyContaining<ValidatorMarker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
