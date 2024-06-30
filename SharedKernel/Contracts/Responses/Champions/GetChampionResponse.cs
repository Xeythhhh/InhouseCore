namespace SharedKernel.Contracts.Responses.Champions;
public sealed record GetChampionResponse(
    long Id,
    string Name,
    string Role);