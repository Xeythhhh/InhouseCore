namespace SharedKernel.Contracts.Requests.Champions;
public sealed record CreateChampionRequest(
    string Name,
    string Role);
