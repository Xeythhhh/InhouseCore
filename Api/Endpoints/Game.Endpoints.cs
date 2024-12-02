using Application.Games.Queries;

using Carter;

using MediatR;

using SharedKernel.Contracts.v1;
using SharedKernel.Contracts.v1.Games;
using SharedKernel.Primitives.Result;

namespace Api.Endpoints;

public class GameEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/games", async (ISender sender) =>
        {
            Result<GetAllGamesResponse> result = await Result.Ok(new GetAllGamesQuery())
                .Bind(query => sender.Send(query));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();
    }
}