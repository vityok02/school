using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Schools;

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

    public static SchoolItemDto ToSchoolItemDto(this School school)
    {
        return new SchoolItemDto(
            school.Id,
            school.Name,
            school.Address.City,
            school.Address.Street);
    }
}
