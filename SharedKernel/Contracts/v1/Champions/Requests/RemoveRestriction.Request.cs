namespace SharedKernel.Contracts.v1.Champions.Requests;

public sealed record RemoveRestrictionRequest(
    long ChampionId,
    long RestrictionId);
