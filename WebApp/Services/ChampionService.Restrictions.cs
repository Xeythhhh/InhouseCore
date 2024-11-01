using SharedKernel.Contracts.v1.Champions.Requests;
using SharedKernel.Primitives.Result;

using System.Net.Http.Json;

using WebApp.Champions.Restrictions.Models;

namespace WebApp.Services;

public sealed partial class ChampionService
{
    /// <summary> Adds a restriction to a champion asynchronously.</summary>
    /// <param name="model">The <see cref="AddRestrictionModel"/> containing details of the restriction to add.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the add operation.</returns>
    public async Task<Result> AddRestrictionAsync(AddRestrictionModel model, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PostAsJsonAsync(
            "champions/add-restriction",
            model.ToRequest(),
            cancellationToken: cancellationToken));

    /// <summary> Updates an existing restriction for a champion asynchronously.</summary>
    /// <param name="model">The <see cref="UpdateRestrictionModel"/> containing the updated restriction information.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the update operation.</returns>
    public async Task<Result> UpdateRestrictionAsync(UpdateRestrictionModel model, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PutAsJsonAsync(
            "champions/update-restriction",
            model.ToRequest(),
            cancellationToken: cancellationToken));

    /// <summary> Removes a restriction from a specified champion asynchronously.</summary>
    /// <param name="championId">The unique identifier of the champion from whom the restriction will be removed.</param>
    /// <param name="restrictionId">The unique identifier of the restriction to be removed.</param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the remove operation.</returns>
    public async Task<Result> RemoveRestrictionAsync(string championId, string restrictionId, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PutAsJsonAsync(
            "champions/remove-restriction",
            new RemoveRestrictionRequest(championId, restrictionId),
            cancellationToken: cancellationToken));
}
