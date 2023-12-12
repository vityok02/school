using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Identity.Requests;

namespace SchoolManagement.API.Identity.Handlers;

public static class ChangePasswordHandler
{
    public static async Task<IResult> Handle(
        HttpContext context,
        [FromBody] ChangePasswordRequest request,
        UserManager<IdentityUser<int>> userManager)
    {
        var user = await userManager.FindByNameAsync(context.User.Identity.Name);

        if (user is null)
        {
            return Results.Unauthorized();
        }

        if (request.NewPassword != request.ConfirmedPassword)
        {
            return Results.Problem();
        }

        var result = await userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);

        if (!result.Succeeded)
        {
            return Results.Problem(string.Join(";", result.Errors.Select(e => $"{e.Code}: {e.Description}")));
        }

        return Results.Ok("The password has been changed");
    }
}