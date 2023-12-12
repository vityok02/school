using SchoolManagement.Models.Constants;
using System.Security.Claims;

namespace SchoolManagement.Models.ClaimGroups;

public static class SystemAdminClaims
{
    public static Claim[] Claims = new[]
    {
        new Claim(ClaimNames.Permissions, Permissions.CanManageSchools),
        new Claim(ClaimNames.Permissions, Permissions.CanManagePositions)
    };
}
