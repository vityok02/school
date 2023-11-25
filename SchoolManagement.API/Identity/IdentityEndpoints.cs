using SchoolManagement.API.Identity.Handlers;

namespace SchoolManagement.API.Identity;

public static class IdentityEndpoints
{
    public static void Map(WebApplication app)
    {
        var identityGroup = app.MapGroup("/");

        identityGroup.MapPost("/reg", RegistrationHandler.Handle);

        identityGroup.MapPost("/log", LoginHandler.Handle);

        identityGroup.MapPost("/claims", AddClaimsHandler.Handle);
    }
}