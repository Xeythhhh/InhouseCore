using System.Net.Http.Json;

using MudBlazor;

using SharedKernel.Contracts.v1.Games;
using SharedKernel.Primitives.Result;

namespace WebApp.Services;

/// <summary> Defines methods for interacting with game-related services.</summary>
public sealed class GameService(HttpClient httpClient, ISnackbar snackbar)
    : HttpService(snackbar)
{
    /// <summary> Retrieves all games asynchronously.</summary>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{GetAllGamesResponse}"/> containing the list of champions if successful, or error information if failed.</returns>
    public async Task<Result<GetAllGamesResponse>> GetGamesAsync(CancellationToken cancellationToken = default) =>
        await ApiRequestNullable(httpClient.GetFromJsonAsync<GetAllGamesResponse>(
            "games", cancellationToken: cancellationToken));
}
