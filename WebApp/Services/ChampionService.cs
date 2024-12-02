using System.Net.Http.Json;

using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Contracts.v1.Champions.Responses;
using SharedKernel.Primitives.Result;

namespace WebApp.Services;

/// <summary> Defines methods for interacting with champion-related services.</summary>
public sealed partial class ChampionService(HttpClient httpClient, ISnackbar snackbar)
    : HttpService(snackbar)
{
    /// <summary> Retrieves a champion by its identifier asynchronously.</summary>
    /// <param name="id">The unique identifier of the champion to retrieve.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{ChampionDto}"/> containing the champion details if successful, or error information if failed.</returns>
    public async Task<Result<ChampionDto>> GetChampionByIdAsync(long id, CancellationToken cancellationToken = default) =>
        await ApiRequestNullable(httpClient.GetFromJsonAsync<ChampionDto>(
            $"champions/{id}", cancellationToken: cancellationToken));

    /// <summary> Retrieves all champions asynchronously.</summary>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{GetAllChampionsResponse}"/> containing the list of champions if successful, or error information if failed.</returns>
    public async Task<Result<GetAllChampionsResponse>> GetChampionsAsync(long gameId = 0, CancellationToken cancellationToken = default) =>
        await ApiRequestNullable(httpClient.GetFromJsonAsync<GetAllChampionsResponse>(
            $"{gameId}/champions", cancellationToken: cancellationToken));
}
