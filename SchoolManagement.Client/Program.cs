using SchoolManagement.Client.Components;
using SchoolManagement.Client.Constants;
using SchoolManagement.Client.Features.Schools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiUrl = Environment.GetEnvironmentVariable("ApiUrl");
builder.Services.AddHttpClient(ApiName.Value, c => c.BaseAddress = new Uri(apiUrl ?? "https://localhost:7128"));

builder.Services.AddScoped<ISchoolService, SchoolService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
