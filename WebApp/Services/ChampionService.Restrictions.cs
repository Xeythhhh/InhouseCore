using SharedKernel.Contracts.v1.Champions.Requests;
using SharedKernel.Primitives.Result;

using System.Net.Http.Json;

using WebApp.Pages.Champions.Dialogs.Restrictions;

namespace WebApp.Services;

public sealed partial class ChampionService
{
    public async Task<Result> AddRestrictionAsync(AddRestrictionModel model, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PostAsJsonAsync(
            "champions/add-restriction",
            model.ToRequest(),
            cancellationToken: cancellationToken));

    public async Task<Result> UpdateRestrictionAsync(UpdateRestrictionModel model, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PutAsJsonAsync(
            "champions/update-restriction",
            model.ToRequest(),
            cancellationToken: cancellationToken));

    public async Task<Result> RemoveRestrictionAsync(string championId, string restrictionId, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PutAsJsonAsync(
            "champions/remove-restriction",
            new RemoveRestrictionRequest(championId, restrictionId),
            cancellationToken: cancellationToken));
}
