namespace SharedKernel.Contracts.Requests.Champions;
public sealed record UpdateChampionRequest(
    long Id,
    string Name,
    string Role);