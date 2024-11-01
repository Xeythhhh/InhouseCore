using System.Net.Http.Json;

using MudBlazor;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Contracts.v1.Champions.Responses;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

using WebApp.Extensions;

namespace WebApp.Services;

/// <summary> Defines methods for interacting with champion-related services.</summary>
public sealed partial class ChampionService(HttpClient httpClient, ISnackbar snackbar)
{
    /// <summary> Executes an HTTP request and processes the result.</summary>
    /// <param name="task">The task representing the HTTP request to execute.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation, with errors displayed via snackbar if failed.</returns>
    public async Task<Result> ApiRequest(Task<HttpResponseMessage> task) =>
        await Result.Try(task)
            .Ensure(response => response is not null, new Error("HTTP Response was null"))
            .Map(response => response!)
            .TapError(snackbar.NotifyErrors)
            .Bind(response => response.HandleHttpErrorResponse(snackbar));

    /// <summary> Executes an HTTP request for nullable results and processes the outcome.</summary>
    /// <typeparam name="T">The type of the expected response.</typeparam>
    /// <param name="task">The task representing the HTTP request to execute.</param>
    /// <returns>A <see cref="Result{T}"/> containing the response if successful, or error information if failed.</returns>
    public async Task<Result<T>> ApiRequestNullable<T>(Task<T?> task) =>
        await Result.Try(task)
            .Ensure(response => response is not null, new Error("HTTP Response was null"))
            .Map(response => response!)
            .TapError(snackbar.NotifyErrors);

    /// <summary> Retrieves a champion by its identifier asynchronously.</summary>
    /// <param name="id">The unique identifier of the champion to retrieve.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{ChampionDto}"/> containing the champion details if successful, or error information if failed.</returns>
    public async Task<Result<ChampionDto>> GetChampionByIdAsync(string id, CancellationToken cancellationToken = default) =>
        await ApiRequestNullable(httpClient.GetFromJsonAsync<ChampionDto>(
            $"champions/{id}", cancellationToken: cancellationToken));

    /// <summary> Retrieves all champions asynchronously.</summary>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{GetAllChampionsResponse}"/> containing the list of champions if successful, or error information if failed.</returns>
    public async Task<Result<GetAllChampionsResponse>> GetChampionsAsync(CancellationToken cancellationToken = default) =>
        await ApiRequestNullable(httpClient.GetFromJsonAsync<GetAllChampionsResponse>(
            "champions", cancellationToken: cancellationToken));

    /// <summary> Creates a new champion asynchronously with the specified name and role.</summary>
    /// <param name="name">The name of the champion to create.</param>
    /// <param name="role">The role assigned to the new champion.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the create operation.</returns>
    public async Task<Result> CreateChampionAsync(string name, string role, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PostAsJsonAsync(
            "champions", new CreateChampionRequest(name, role), cancellationToken: cancellationToken));

    /// <summary> Deletes a champion asynchronously by its identifier.</summary>
    /// <param name="id">The unique identifier of the champion to delete.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the delete operation.</returns>
    public async Task<Result> DeleteChampionAsync(string id, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.DeleteAsync(
            $"champions/{id}", cancellationToken: cancellationToken));
}
