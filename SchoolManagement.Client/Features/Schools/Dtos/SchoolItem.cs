namespace SchoolManagement.Client.Features.Schools.Dtos;

public record SchoolItem(
    int Id,
    string Name,
    string City,
    string Street,
    DateTime OpeningDate);
