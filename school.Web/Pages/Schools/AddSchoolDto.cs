namespace SchoolManagement.Web.Pages.Schools;

public record AddSchoolDto(string Name, string Country, string City, string Street, int PostalCode, DateOnly OpeningDate);
