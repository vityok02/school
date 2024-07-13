namespace SchoolManagement.API.Features.Schools.Dtos;

public record SchoolDto(
    int Id,
    string Name,
    string Country,
    string City,
    string Street,
    int PostalCode,
    DateTime OpeningDate);
