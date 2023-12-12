using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.API.Identity.Requests;

namespace SchoolManagement.API.Identity.Handlers;

public static class ResetPasswordHandler
{
    public static async Task<IResult> Handle(
        UserManager<IdentityUser<int>> userManager,
        ResetPasswordRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            return Results.NotFound("User is not found");
        }

        var resetToken = await userManager.GeneratePasswordResetTokenAsync(user!);

        var responseObject = new
        {
            UserName = request.UserName,
            Token = resetToken
        };

        return Results.Ok(responseObject);
    }
}