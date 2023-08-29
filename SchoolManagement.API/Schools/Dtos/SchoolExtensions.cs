using SchoolManagement.Models;

namespace SchoolManagement.API.Schools.Dtos;

public static class SchoolExtensions
{
    public static SchoolItemDto ToSchoolItemDto(this School school)
    {
        return new SchoolItemDto(
            school.Id,
            school.Name,
            school.Address.City,
            school.Address.Street,
            school.OpeningDate);
    }

    public static SchoolInfoDto ToSchoolInfoDto(this School school)
    {
        return new SchoolInfoDto(
            school.Id,
            school.Name,
            school.Address.Country,
            school.Address.City,
            school.Address.Street,
            school.Address.PostalCode,
            school.OpeningDate);
    }
}
