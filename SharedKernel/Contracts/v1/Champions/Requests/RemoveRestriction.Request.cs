namespace SharedKernel.Contracts.v1.Champions.Requests;

public sealed record RemoveRestrictionRequest(
    string ChampionId,
    string RestrictionId);
