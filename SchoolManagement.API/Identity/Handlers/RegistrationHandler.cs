using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.API.Identity.Handlers;

public static class RegistrationHandler
{
    public static async Task<IResult> Handle(
        UserManager<IdentityUser<int>> userManager,
        SignInManager<IdentityUser<int>> signInManager,
        [FromBody] AuthRequest request)
    {
        var user = new IdentityUser<int> { UserName = request.UserName};
        
        var result = await userManager.CreateAsync(user, request.Password);

        if(!result.Succeeded)
        {
            return Results.Problem(string.Join(";", result.Errors.Select(e => e.Description)));
        }

        await signInManager.SignInAsync(user, false);

        return Results.Ok();
    }
}