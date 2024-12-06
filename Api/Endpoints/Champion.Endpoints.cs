using Application.Champions.Queries;

using Carter;

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
        app.MapGet("api/{gameId}/champions", async (long gameId, ISender sender) =>
        {
            Result<GetAllChampionsResponse> result = await Result.Ok(new GetAllChampionsQuery(gameId))
                .Bind(query => sender.Send(query));

            //Todo extension method or implicit conversion or something to Http Result
            //to remove this ugly conditional duplication from all endpoints
            //result.ToHttpResponse() and result.ToHttpResponse(value) MAYBE LOOKS PRETTY GOOD ? :D
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