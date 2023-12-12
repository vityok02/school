namespace SchoolManagement.API.Identity;

public class JwtToken
{
    public string? Token { get; set; }
    public TimeSpan ExpiresIn { get; set; }
}
