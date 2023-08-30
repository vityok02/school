using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers
{
    public class CreatePositionHandler
    {
        public static async Task<IResult> Handle(
            IPositionRepository positionRepository,
            ISchoolRepository schoolRepository,
            [FromRoute] int schoolId,
            [FromBody] PositionCreateDto positionDto)
        {
            var position = new Position()
            {
                Name = positionDto.Name,
            };

            var school = await schoolRepository.GetAsync(schoolId);

            if(school is null)
            {
                return Results.NotFound("The school you want to add a position to is not found");
            }

            position.Schools.Add(school);

            await positionRepository.AddAsync(position);

            var createdPositionDto = position.ToPositionDto();
            return Results.Created($"/schools/{schoolId}/positions/{position.Id}", createdPositionDto);
        }
    }
}
