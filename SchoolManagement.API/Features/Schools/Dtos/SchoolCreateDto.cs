namespace SchoolManagement.API.Features.Schools.Dtos;

public record SchoolCreateDto(
    string Name,
    string Country,
    string City,
    string Street,
    int PostalCode,
    DateTime OpeningDate)
    : ISchoolDto;
