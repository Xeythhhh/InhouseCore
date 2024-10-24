namespace SharedKernel.Contracts.v1.Champions;

public sealed record RemoveRestrictionRequest(
    string ChampionId,
    string RestrictionId);
