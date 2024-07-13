using SchoolManagement.Models;

namespace SchoolManagement.API.Features.Schools.Dtos;

public static class SchoolExtensions
{
    public static SchoolDto ToSchoolDto(this School school)
    {
        return new SchoolDto(
            school.Id,
            school.Name,
            school.Address.Country,
            school.Address.City,
            school.Address.Street,
            school.Address.PostalCode,
            school.OpeningDate);
    }
}
