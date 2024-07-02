using Application.Champions.Commands;
using Application.Champions.Queries;

using Carter;

using Domain.Champions;

using SharedKernel.Primitives.Result;

using MediatR;

using SharedKernel.Contracts.Requests.Champions;
using SharedKernel.Contracts.Responses.Champions;

namespace Api.Endpoints;

public class ChampionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/champions", async (CreateChampionRequest request, ISender sender) =>
        {
            Result<ChampionId> result = await CreateChampionCommand.FromRequest(request)
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Errors);
        }).WithOpenApi();

        app.MapPut("api/champions", async (UpdateChampionRequest request, ISender sender) =>
        {
            Result<ChampionId> result = await UpdateChampionCommand.FromRequest(request)
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Errors);
        }).WithOpenApi();

        app.MapDelete("api/champions/{id}", async (long id, ISender sender) =>
        {
            Result result = await Result.Ok(new DeleteChampionCommand(id))
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(result.Errors);
        }).WithOpenApi();

        app.MapGet("api/champions", async (ISender sender) =>
        {
            Result<GetAllChampionsResponse> result = await Result.Ok(new GetAllChampionsQuery())
                .Bind(query => sender.Send(query));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Errors);
        }).WithOpenApi();

        app.MapGet("api/champions/{id}", async (long id, ISender sender) =>
        {
            Result<GetChampionResponse> result = await Result.Ok(new GetChampionQuery(id))
                .Bind(query => sender.Send(query));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Errors);
        }).WithOpenApi();
    }
}