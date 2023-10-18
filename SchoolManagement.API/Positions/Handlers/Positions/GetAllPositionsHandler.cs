using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Positions.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Positions.Handlers.Positions;

public static class GetAllPositionsHandler
{
    public static async Task<IResult> Handle(IPositionRepository repository)
    {
        var positions = await repository.GetAllAsync();

        var positionItemDtos = positions.Select(p => p.ToPositionDto());
        return Results.Ok(positionItemDtos);
    }
}
