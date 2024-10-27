using System.Net.Http.Json;

using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Contracts.v1.Champions.Responses;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

using WebApp.Extensions;

namespace WebApp.Services;

public sealed partial class ChampionService(HttpClient httpClient, ISnackbar snackbar) : IChampionService
{
    public async Task<Result> ApiRequest(Task<HttpResponseMessage> task) =>
        await Result.Try(task)
            .Ensure(response => response is not null, new Error("HTTP Response was null"))
            .Map(response => response!)
            .TapError(snackbar.NotifyErrors)
            .Bind(response => response.HandleHttpErrorResponse(snackbar));

    public async Task<Result<T>> ApiRequestNullable<T>(Task<T?> task) =>
        await Result.Try(task)
            .Ensure(response => response is not null, new Error("HTTP Response was null"))
            .Map(response => response!)
            .TapError(snackbar.NotifyErrors);

    public async Task<Result<ChampionDto>> GetChampionByIdAsync(string id, CancellationToken cancellationToken = default) =>
        await ApiRequestNullable(httpClient.GetFromJsonAsync<ChampionDto>(
            $"champions/{id}", cancellationToken: cancellationToken));

    public async Task<Result<GetAllChampionsResponse>> GetChampionsAsync(CancellationToken cancellationToken = default) =>
        await ApiRequestNullable(httpClient.GetFromJsonAsync<GetAllChampionsResponse>(
            "champions", cancellationToken: cancellationToken));

    public async Task<Result> CreateChampionAsync(string name, string role, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PostAsJsonAsync(
            "champions", new CreateChampionRequest(name, role), cancellationToken: cancellationToken));

    public async Task<Result> DeleteChampionAsync(string id, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.DeleteAsync(
            $"champions/{id}", cancellationToken: cancellationToken));
}