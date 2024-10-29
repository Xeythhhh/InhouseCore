using Application.Champions.Commands;

using Carter;

using SharedKernel.Primitives.Result;

using MediatR;
using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Contracts.v1;
using SharedKernel.Contracts.v1.Champions.Requests;

namespace Api.Endpoints;

public class ChampionRestrictionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/champions/add-restriction", async (AddRestrictionRequest request, ISender sender) =>
        {
            Result result = await AddRestrictionCommand.FromRequest(request)
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();

        app.MapPut("api/champions/update-restriction", async (UpdateRestrictionRequest request, ISender sender) =>
        {
            Result result = await UpdateRestrictionCommand.FromRequest(request)
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();

        app.MapPut("api/champions/remove-restriction", async (RemoveRestrictionRequest request, ISender sender) =>
        {
            Result result = await RemoveRestrictionCommand.FromRequest(request)
                .Bind(command => sender.Send(command));

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(ErrorResponse.FromResult(result));
        }).WithOpenApi();
    }
}
