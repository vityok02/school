using SchoolManagement.API.Abstractions;

namespace SchoolManagement.API.Features.Schools;

public static class SchoolErrors
{
    public static readonly Error Dublicate = new(
        "Schools.Dublicate",
        "A school with this data set already exists");

    public static readonly Error NotFound = new(
        "Schools.NotFound",
        "No such school found");

    public static Error NotValid(string error) => new(
        "Schools.NotValid",
        error);
}