namespace SchoolManagement.Models.Dto
{
    public static class SchoolExtension
    {
        public static SchoolDto ToSchoolDto(this School school)
        {
            return new SchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                Country = school.Address.Country,
                City = school.Address.City,
                Street = school.Address.Street,
                PostalCode = school.Address.PostalCode,
                OpeningDate = DateOnly.FromDateTime(school.OpeningDate)
            };
        }
    }
}
