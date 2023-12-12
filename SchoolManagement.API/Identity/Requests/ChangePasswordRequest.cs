namespace SchoolManagement.API.Identity.Requests;

public sealed record ChangePasswordRequest(string Password, string NewPassword, string ConfirmedPassword);