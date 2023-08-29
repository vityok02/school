namespace SchoolManagement.API.Schools.Dtos;

public record SchoolItemDto(
    int Id,
    string Name,
    string City,
    string Street,
    DateTime OpeningDate);
