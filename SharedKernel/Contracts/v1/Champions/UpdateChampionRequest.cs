namespace SharedKernel.Contracts.v1.Champions;
public sealed record UpdateChampionRequest(
    long Id,
    IEnumerable<ChampionRestrictionDto>? Restrictions = null);
