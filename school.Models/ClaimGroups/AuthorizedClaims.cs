using SchoolManagement.Models.Constants;
using System.Security.Claims;

namespace SchoolManagement.Models.ClaimGroups;

public class AuthorizedClaims
{
    public static Claim[] Claims = new[]
    {
        new Claim(ClaimNames.Permissions, Permissions.CanViewInfo)
    };
}
