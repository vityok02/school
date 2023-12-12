using Azure.Core;

namespace SchoolManagement.API.Identity.Requests;

public record AuthRequest(string UserName, string Password)
{
    public bool IsAdmin() => UserName == "admin" && Password == "Pass@word1";
}