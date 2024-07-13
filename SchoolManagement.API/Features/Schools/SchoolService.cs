using FluentValidation;
using SchoolManagement.API.Abstractions;
using SchoolManagement.API.Features.Schools.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.API.Features.Schools;

public class SchoolService : ISchoolService
{
    private readonly ISchoolRepository _repository;
    private readonly IValidator<ISchoolDto> _validator;
    private readonly LinkGenerator _linkGenerator;

    public SchoolService(
        ISchoolRepository repository,
        IValidator<ISchoolDto> validator,
        LinkGenerator linkGenerator)
    {
        _repository = repository;
        _validator = validator;
        _linkGenerator = linkGenerator;
    }

    public async Task<PagedList<SchoolDto>> GetSchools(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize)
    {
        var schoolsQuery = _repository
            .GetSchoolsQuery(searchTerm, sortColumn, sortOrder)
            .Select(s => s.ToSchoolDto());

        var schools = await PagedList<SchoolDto>
            .CreateAsync(schoolsQuery, page, pageSize);

        return schools;
    }

    public async Task<SchoolDto> GetSchoolById(int id)
    {
        var school = await _repository.GetSchoolAsync(id);

        return school.ToSchoolDto();
    }

    public async Task<(bool isSuccess, int? errorCode, string? description)> CreateSchool(HttpContext context, SchoolCreateDto schoolDto)
    {
        var validationResult = await _validator.ValidateAsync(schoolDto);

        if (!validationResult.IsValid)
        {
            return (false, StatusCodes.Status400BadRequest, validationResult.ToString());
            //return Results.ValidationProblem(validationResult.ToDictionary());
        }

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

        await _repository.AddAsync(school);

        var uri = _linkGenerator.GetUriByName(context, "SchoolInfo", new
        {
            schoolId = school.Id
        });

        return new CreatedLink<SchoolDto>(uri, school.ToSchoolDto());
    }

    //public Task<SchoolUpdateDto> UpdateSchool(int id, SchoolUpdateDto school)
    //{


    //    school.Name = schoolDto.Name;
    //    school.Address.Country = schoolDto.Country;
    //    school.Address.City = schoolDto.City;
    //    school.Address.Street = schoolDto.Street;
    //    school.Address.PostalCode = schoolDto.PostalCode;
    //    school.OpeningDate = schoolDto.OpeningDate;

    //    await repository.UpdateAsync(school);

    //    var updatedSchoolDto = school.ToSchoolDto();
    //}

    public Task DeleteSchool(int id)
    {
        throw new NotImplementedException();
    }
}

public record CreatedLink<T>(string? Uri, T Value);