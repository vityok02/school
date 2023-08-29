namespace SchoolManagement.API.Schools.Dtos;

public record SchoolInfoDto(
    int Id,
    string Name,
    string Country,
    string City,
    string Street,
    int PostalCode,
    DateTime OpeningDate);
