using Microsoft.AspNetCore.Identity;
using SchoolManagement.Models.ClaimGroups;

namespace SchoolManagement.Data;

public class AdminSeeder
{
    public static async Task SeedAdmin(UserManager<IdentityUser<int>> userManager)
    {
        if (await userManager.FindByNameAsync("admin") is not null)
        {
            return;
        }

        IdentityUser<int> admin = new()
        {
            UserName = "admin",
            LockoutEnabled = false
        };

        var result = await userManager.CreateAsync(admin, "Pass@word1");

        if (!result.Succeeded)
        {
            return;
        }

        await userManager.AddClaimsAsync(admin, SystemAdminClaims.Claims);
    }
}
