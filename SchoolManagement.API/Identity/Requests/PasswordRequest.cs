namespace SchoolManagement.API.Identity.Requests;

public record PasswordRequest(string NewPassword, string ConfirmedPassword)
{
    public bool IsPasswordConfirmed() => NewPassword == ConfirmedPassword;
}
