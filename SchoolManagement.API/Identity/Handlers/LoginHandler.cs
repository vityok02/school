using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.API.Identity.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace SchoolManagement.API.Identity.Handlers;

public static class LoginHandler
{
    public static async Task<IResult> Handle(
        UserManager<IdentityUser<int>> userManager,
        SignInManager<IdentityUser<int>> signInManager,
        [FromBody] AuthRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user is null) 
        {
            return Results.Unauthorized();
        }

        var result = await signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            lockoutOnFailure: false);

        if (!result.Succeeded) 
        {
            return Results.Unauthorized();
        }

        if (request.IsAdmin())
        {
            var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);

            return Results.Problem(
                "User must change the default password first",
                instance: JsonSerializer.Serialize(new
                {
                    url = $"/change-password?token={passwordResetToken}"
                }),
                statusCode: StatusCodes.Status307TemporaryRedirect);
        }

        var userClaims = await userManager.GetClaimsAsync(user);

        var claims = new List<Claim> 
        { 
            new Claim(ClaimTypes.Name, request.UserName) 
        };

        claims.AddRange(userClaims);

        var expiresIn = TimeSpan.FromMinutes(20);
        var expiresAt = DateTime.UtcNow.Add(expiresIn);

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        string token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return Results.Ok(new
        {
            access_token = token,
            expiresIn = expiresIn.TotalSeconds
        });
    }
}