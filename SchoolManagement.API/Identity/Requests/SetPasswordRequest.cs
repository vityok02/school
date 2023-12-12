namespace SchoolManagement.API.Identity.Requests;

public sealed record SetPasswordRequest(string UserName, string Token, string NewPassword, string ConfirmedPassword);
