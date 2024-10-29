using SharedKernel.Contracts.v1.Champions.Requests;
using SharedKernel.Contracts.v1.Champions.Responses;
using SharedKernel.Primitives.Result;

using System.Net.Http.Json;
using WebApp.Champions.Augments.Models;

namespace WebApp.Services;

public sealed partial class ChampionService
{
    public async Task<Result<GetChampionAugmentNamesResponse>> GetAugmentNamesAsync(string championId, CancellationToken cancellationToken = default) =>
        await ApiRequestNullable(httpClient.GetFromJsonAsync<GetChampionAugmentNamesResponse>(
            $"champions/augment-names/{championId}", cancellationToken: cancellationToken));

    public async Task<Result> AddAugmentAsync(AddAugmentModel model, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PostAsJsonAsync(
            "champions/add-augment",
             model.ToRequest(),
            cancellationToken: cancellationToken));

    public async Task<Result> UpdateAugmentAsync(UpdateAugmentModel model, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PutAsJsonAsync(
            "champions/update-augment",
            model.ToRequest(),
            cancellationToken: cancellationToken));

    public async Task<Result> RemoveAugmentAsync(string championId, string augmentId, CancellationToken cancellationToken = default) =>
        await ApiRequest(httpClient.PutAsJsonAsync(
            "champions/remove-augment",
            new RemoveAugmentRequest(championId, augmentId),
            cancellationToken: cancellationToken));
}
