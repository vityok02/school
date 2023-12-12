using SchoolManagement.API.Identity.Handlers;

namespace SchoolManagement.API.Identity;

public static class IdentityEndpoints
{
    public static void Map(WebApplication app)
    {
        var identityGroup = app.MapGroup("/")
            .WithTags("Identity")
            .WithOpenApi();

        identityGroup.MapPost("/reg", RegistrationHandler.Handle)
            .WithSummary("Register");

        identityGroup.MapPost("/log", LoginHandler.Handle)
            .WithSummary("Login");

        identityGroup.MapPost("/claims", AddClaimsHandler.Handle)
            .WithSummary("Add claims");

        identityGroup.MapPost("/reset-password", ResetPasswordHandler.Handle)
            .WithSummary("Reset password");

        identityGroup.MapPost("/change-password", ChangePasswordHandler.Handle)
            .WithSummary("Change password")
            .RequireAuthorization();

        identityGroup.MapPost("/set-password", SetPasswordHandler.Handle)
            .WithSummary("Set password");
    }
}