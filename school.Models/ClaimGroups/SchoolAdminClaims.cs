using SchoolManagement.Models.Constants;
using System.Security.Claims;

namespace SchoolManagement.Models.ClaimGroups;

public static class SchoolAdminClaims
{
    public static Claim[] Claims = new[]
    {
        new Claim(ClaimNames.Permissions, Permissions.CanManageSchoolUsers),
        new Claim(ClaimNames.Permissions, Permissions.CanManageEmployees),
        new Claim(ClaimNames.Permissions, Permissions.CanManageFloors),
        new Claim(ClaimNames.Permissions, Permissions.CanManageRooms),
        new Claim(ClaimNames.Permissions, Permissions.CanManageSchoolPositions),
        new Claim(ClaimNames.Permissions, Permissions.CanManageStudents)
    };
}
