using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Constants;
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

        foreach(var claim in claimRequest.Claims)
        {
            await userManager.AddClaimAsync(user, new Claim(claim.Key, claim.Value));
        }

        return Results.Empty;

        //SysAdmin can give SchoolAdmin only
        if (context.User.HasClaim(ClaimNames.Permissions, Permissions.CanManageSchoolAdmin))
        {
            foreach(var claim in claimRequest.Claims)
            {
                await userManager.AddClaimAsync(user, new Claim(ClaimNames.Permissions, ClaimValues.SchoolAdmin));
            }
        }

        //SchoolAdmin can give Employee only

        //var admin = context.User.FindAll(c => c.Type == ClaimNames.Admin);

        //var result = await AssignClaims(ClaimValues.SchoolAdmin, ClaimNames.Employee);

        //if (result != Results.Empty)
        //{
        //    return result;
        //}

        //result = await AssignClaims(ClaimValues.SystemAdmin, ClaimNames.Admin);

        //if (result != Results.Empty)
        //{
        //    return result;
        //}

        //return Results.Ok();

        //async Task<IResult> AssignClaims(string adminType, string type)
        //{
        //    if (!admin.Any(ac => ac.Value == adminType))
        //    {
        //        return Results.Empty;
        //    }

        //    foreach (var claim in claimRequest.Claims.Where(c => c.Key == type))
        //    {
        //        var result = await userManager.AddClaimAsync(user, new Claim(claim.Key, claim.Value));

        //        if (!result.Succeeded)
        //        {
        //            return Results.Problem(string.Join(";", result.Errors.Select(e => $"{e.Code}: {e.Description}")));
        //        }
        //    }

        //    return Results.Empty;
        //}
    }
}

public sealed record UserClaimRequest(string Username, CustomClaim[] Claims);

public sealed record CustomClaim(string Key, string Value);