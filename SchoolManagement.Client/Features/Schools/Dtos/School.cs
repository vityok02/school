namespace SchoolManagement.Client.Features.Schools.Dtos;

public class School
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int PostalCode { get; set; }
    public DateTime OpeningDate { get; set; }
}