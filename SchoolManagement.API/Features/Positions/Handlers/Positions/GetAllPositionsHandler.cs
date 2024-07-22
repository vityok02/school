using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Features.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Features.Positions.Handlers.Positions;

public static class GetAllPositionsHandler
{
    public static async Task<IResult> Handle(IPositionRepository repository,
        [FromQuery] string? searchTerm,
        [FromQuery] string? sortOrder,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var positionsQuery = repository
            .GetAllPositionsQueryable(searchTerm, sortOrder)
            .Select(p => p.ToPositionDto());

        var positions = await PagedList<PositionDto>.CreateAsync(positionsQuery, page, pageSize);

        return Results.Ok(positions);
    }
}
