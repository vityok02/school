namespace SchoolManagement.API.Schools.Dtos;

public record SchoolDetailsDto(
    int Id,
    string Name,
    string Country,
    string City,
    string Street,
    int PostalCode,
    DateTime OpeningDate);
