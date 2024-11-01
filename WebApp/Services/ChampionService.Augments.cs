using SharedKernel.Contracts.v1.Champions.Requests;
using SharedKernel.Contracts.v1.Champions.Responses;
using SharedKernel.Primitives.Result;

using System.Net.Http.Json;

using WebApp.Champions.Augments.Models;

namespace WebApp.Services;

public sealed partial class ChampionService
{
    /// <summary> Retrieves the augment names for a specified champion asynchronously.</summary>
    /// <param name="championId">The unique identifier of the champion whose augment names are being retrieved.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{GetChampionAugmentNamesResponse}"/> containing the augment names if successful, or error information if failed.</returns>
    public async Task<Result<GetChampionAugmentNamesResponse>> GetAugmentNamesAsync(string championId, CancellationToken cancellationToken = default) =>
        await ApiRequestNullable(httpClient.GetFromJsonAsync<GetChampionAugmentNamesResponse>(
            $"champions/augment-names/{championId}", cancellationToken: cancellationToken));

    /// <summary> Adds a new augment to a specified champion asynchronously.</summary>
    /// <param name="model">The <see cref="AddAugmentModel"/> containing details of the augment to add.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the add operation.</returns>
    public async Task<Result> AddAugmentAsync(AddAugmentModel model, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PostAsJsonAsync(
            "champions/add-augment",
            model.ToRequest(),
            cancellationToken: cancellationToken));

    /// <summary> Updates an existing augment for a specified champion asynchronously.</summary>
    /// <param name="model">The <see cref="UpdateAugmentModel"/> containing the updated augment information.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the update operation.</returns>
    public async Task<Result> UpdateAugmentAsync(UpdateAugmentModel model, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PutAsJsonAsync(
            "champions/update-augment",
            model.ToRequest(),
            cancellationToken: cancellationToken));

    /// <summary> Removes an augment from a specified champion asynchronously.</summary>
    /// <param name="championId">The unique identifier of the champion from whom the augment will be removed.</param>
    /// <param name="augmentId">The unique identifier of the augment to remove.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the remove operation.</returns>
    public async Task<Result> RemoveAugmentAsync(string championId, string augmentId, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PutAsJsonAsync(
            "champions/remove-augment",
            new RemoveAugmentRequest(championId, augmentId),
            cancellationToken: cancellationToken));
}
