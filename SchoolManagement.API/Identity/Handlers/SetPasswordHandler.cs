using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Identity.Requests;

namespace SchoolManagement.API.Identity.Handlers;

public static class SetPasswordHandler
{
    public static async Task<IResult> Handle(
        UserManager<IdentityUser<int>> userManager,
        [FromBody] SetPasswordRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            return Results.NotFound("User is not found");
        }

        if (!request.IsPasswordConfirmed())
        {
            return Results.Problem("Passwords are not equal");
        }

        var result = await userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!result.Succeeded) 
        {
            return Results.Problem(string.Join(";", result.Errors.Select(e => $"{e.Code}: {e.Description}")));
        }

        return Results.Ok("Password set succesfully");
    }
}