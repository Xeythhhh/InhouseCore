using Application.Champions.Commands;
using Application.Champions.Queries;

using Carter;

using Domain.Champions;

using FluentResults;

using MediatR;

using SharedKernel.Champions;
using SharedKernel.Extensions.ResultExtensions;

namespace Host.Endpoints;

public class ChampionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/champions", async (CreateChampionDto dto, ISender sender) =>
        {
            Result<ChampionId> result = await CreateChampionCommand.FromDto(dto)
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Errors);
        });

        app.MapGet("api/champions", async (ISender sender) =>
        {
            Result<List<GetChampionDto>> result = await Result.Ok(new GetAllChampionsQuery())
                .Bind(query => sender.Send(query));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Errors);
        });
    }
}