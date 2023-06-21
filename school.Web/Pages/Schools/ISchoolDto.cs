namespace SchoolManagement.Web.Pages.Schools;

public interface ISchoolDto
{
    string Name { get; }
    string Country { get; }
    string City { get; }
    string Street { get; }
    int PostalCode { get; }
    DateTime OpeningDate { get; }
}
