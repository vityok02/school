namespace SchoolManagement.API.Features.Schools.Dtos;

public record SchoolUpdateDto(
    string Name,
    string Country,
    string City,
    string Street,
    int PostalCode,
    DateTime OpeningDate)
    : ISchoolDto;
