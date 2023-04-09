namespace SchoolManagement.Web.Pages.Schools;

public record SchoolDto(int Id, string Name, string Country, string City, string Street, int PostalCode, DateOnly OpeningDate);