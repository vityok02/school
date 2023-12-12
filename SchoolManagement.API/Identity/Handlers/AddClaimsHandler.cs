using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.ClaimGroups;
using SchoolManagement.Models.Constants;
using System.Security.Claims;

namespace SchoolManagement.API.Identity.Handlers;

class AddClaimsHandler
{
    public static async Task<IResult> Handle(
        HttpContext context,
        UserManager<IdentityUser<int>> userManager,
        [FromBody] UserClaimRequest claimRequest)
    {
        var user = await userManager.FindByNameAsync(claimRequest.Username);

        if (user is null)
        {
            return Results.Unauthorized();
        }

        var userClaims = await userManager.GetClaimsAsync(user);

        if (context.User.HasClaim(ClaimNames.Permissions, Permissions.CanManageSchools))
        {
            foreach (var claim in SchoolAdminClaims.Claims)
            {
                if(!userClaims.Any(c => c.Value == claim.Value))
                {
                    await userManager.AddClaimAsync(user, claim);
                }
            }
        }

        if (context.User.HasClaim(ClaimNames.Permissions, Permissions.CanManageEmployees))
        {
            foreach (var claim in claimRequest.Claims)
            {
                await userManager.AddClaimAsync(user, new Claim(ClaimNames.Permissions, Permissions.CanManageSchoolUsers));
            }
        }

        return Results.Empty;
    }
}

public sealed record UserClaimRequest(string Username, CustomClaim[] Claims);

public sealed record CustomClaim(string Key, string Value);