using Application.Champions.Commands;

using Carter;

using SharedKernel.Primitives.Result;

using MediatR;
using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Contracts.v1;
using SharedKernel.Contracts.v1.Champions.Requests;
using Application.Champions.Queries;
using SharedKernel.Contracts.v1.Champions.Responses;

namespace Api.Endpoints;

public class ChampionAugmentEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/champions/add-augment", async (AddAugmentRequest request, ISender sender) =>
        {
            Result result = await AddAugmentCommand.FromRequest(request)
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();

        app.MapPut("api/champions/update-augment", async (UpdateAugmentRequest request, ISender sender) =>
        {
            Result result = await UpdateAugmentCommand.FromRequest(request)
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();

        app.MapPut("api/champions/remove-augment", async (RemoveAugmentRequest request, ISender sender) =>
        {
            Result result = await RemoveAugmentCommand.FromRequest(request)
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();

        app.MapGet("api/champions/augment-names/{id}", async (long id, ISender sender) =>
        {
            Result<GetChampionAugmentNamesResponse> result = await Result.Ok(new GetChampionAugmentNamesQuery(id))
                .Bind(query => sender.Send(query));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();
    }
}