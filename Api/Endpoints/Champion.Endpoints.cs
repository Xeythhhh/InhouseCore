using Application.Champions.Commands;
using Application.Champions.Queries;

using Carter;

using Domain.Champions;

using SharedKernel.Primitives.Result;

using MediatR;
using SharedKernel.Contracts.v1;
using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Contracts.v1.Champions.Responses;

namespace Api.Endpoints;

public class ChampionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/champions", async (CreateChampionRequest request, ISender sender) =>
        {
            Result<Champion.ChampionId> result = await CreateChampionCommand.FromRequest(request)
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();

        app.MapDelete("api/champions/{id}", async (string id, ISender sender) =>
        {
            Result result = await Result.Ok(new DeleteChampionCommand(long.Parse(id)))
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();

        app.MapGet("api/champions", async (ISender sender) =>
        {
            Result<GetAllChampionsResponse> result = await Result.Ok(new GetAllChampionsQuery())
                .Bind(query => sender.Send(query));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();

        app.MapGet("api/champions/{id}", async (long id, ISender sender) =>
        {
            Result<ChampionDto> result = await Result.Ok(new GetChampionQuery(id))
                .Bind(query => sender.Send(query));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();
    }
}