using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Positions.Handlers.SchoolPositions;

public static class GetSchoolPositionsHandler
{
    public static async Task<IResult> Handle(IPositionRepository repository,
        [FromRoute] int schoolId,
        [FromQuery] string? searchTerm,
        [FromQuery] string? sortOrder,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var positionsQuery = repository.GetAllPositionsQueryable(searchTerm, sortOrder, schoolId)
            .Select(p => p.ToPositionDto());

        var positions = await PagedList<PositionDto>.CreateAsync(positionsQuery, page, pageSize);
        
        return Results.Ok(positions);
    }
}
