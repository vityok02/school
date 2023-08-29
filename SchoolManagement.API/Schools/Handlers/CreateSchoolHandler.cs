using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Schools.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Schools.Handlers;

public static class CreateSchoolHandler
{
    public static async Task<IResult> Handle(ISchoolRepository repository, [FromBody] SchoolCreateDto schoolDto)
    {
        var address = new Address()
        {
            Country = schoolDto.Country,
            City = schoolDto.City,
            Street = schoolDto.Street,
            PostalCode = schoolDto.PostalCode,
        };

        var school = new School()
        {
            Name = schoolDto.Name,
            Address = address,
            OpeningDate = schoolDto.OpeningDate,
        };

        await repository.AddAsync(school);
        
        var createdSchoolDto = school.ToSchoolInfoDto();
        return Results.Created($"/schools/{school.Id}", createdSchoolDto);
    }
}
