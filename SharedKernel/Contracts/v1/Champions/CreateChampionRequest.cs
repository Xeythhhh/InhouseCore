namespace SharedKernel.Contracts.v1.Champions;
public sealed record CreateChampionRequest(
    string Name,
    string Role,
    IEnumerable<ChampionRestrictionDto>? Restrictions = null);
